using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;
using Lagalt_Backend.Services;
using Lagalt_Backend.Exceptions;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LagaltDbContext _context;

        public UsersController(IUserService userService, LagaltDbContext context) {
            _userService = userService;
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
            return Ok(await _userService.GetAllUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id) {
            try {
                return await _userService.GetUserById(id);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user) {
            if (id != user.Id) {
                return BadRequest();
            }

            try {
                await _userService.UpdateUser(user);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }

        [HttpPut("{id}/AddSkill")]
        public async Task<ActionResult> AddSkillToUser(int id, string skill) {
            if (skill == null || skill == "") {
                return BadRequest("not a valid skill");
            }

            var user = await _userService.GetUserById(id);
            if (id != user.Id) {
                return NotFound();
            }

            if(user.Skills.Any(s => s.Name == skill)) {
                return BadRequest("the skill is already in user");
            }


            //See if skills exist 
            var addSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skill);
            if(addSkill == null) {
                Skill newSkill = new Skill { Name = skill };
                user.Skills.Add(newSkill);
            }
            else {
                user.Skills.Add(addSkill);
            }

            try {
                await _userService.UpdateUser(user);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }
        [HttpPut("{id}/RemoveSkill")]
        public async Task<ActionResult> RemoveSkillToUser(int id, string skill) {
            var user = await _userService.GetUserById(id);
            if (id != user.Id) {
                return NotFound();
            }

            if(!user.Skills.Any(s => s.Name == skill)) {
                return NotFound();
            }
            user.Skills.RemoveAll(s => s.Name == skill);

            try {
                await _userService.UpdateUser(user);
                await _userService.RemoveSkillIfLast(skill);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user) {
            return CreatedAtAction("GetUser", new { id = user.Id }, await _userService.AddUser(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id) {
            try {
                await _userService.DeleteUser(id);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }
    }
}
