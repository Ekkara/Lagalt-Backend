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
using Lagalt_Backend.Models.DTO.User;
using Lagalt_Backend.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using Lagalt_Backend.Helpers;

namespace Lagalt_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserService userService, LagaltDbContext context) {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
            return Ok(await _userService.GetAllUsers());
        }

        [Authorize]
        [HttpGet("/GetId")]
        public async Task<ActionResult<int>> GetUserId(string keycloakId, string username) {
            var user = await _userService.GetUserAsyncKeycloak(keycloakId, username);
            return Ok(user.Id);
        }
        //[HttpGet("GetUserprofile")]
        //public async Task<ActionResult> GetUserProfile()
        //{
        //    var keycloakId = User.GetId();
        //    var username = User.GetUsername();

        //    if (string.IsNullOrEmpty(keycloakId) || string.IsNullOrEmpty(username))
        //    {
        //        return BadRequest();
        //    }

        //    var user = await _userService.GetUserAsyncKeycloak(keycloakId, username);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var profileUrl = Url.Action("GetUser", "Users", new { id = user.Id }, Request.Scheme);
        //    Response.Headers.Add("Location", profileUrl);
        //    return StatusCode(303);
        //}
        [Authorize]
        [HttpPut("{id}/UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, EditUserDTO userDTO)
        {
            var user = await _userService.GetUserById(id);
            if (user == null || user.Id != id) { return BadRequest("User not found"); }

            user.Description = userDTO.Description;
            user.IsProfileHiden = userDTO.IsProfileHiden;

            try {
                await _userService.UpdateUser(user);
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
            //var keycloakId = User.GetId();
            //if (string.IsNullOrEmpty(keycloakId))
            //{
            //    return BadRequest("Invalid Keycloak ID.");
            //}

            //var userToPatch = _userService.GetUserFromKeyCloak(keycloakId);
            //if (userToPatch == null)
            //{
            //    return NotFound("User not found.");
            //}

            //if (userToPatch.Id != id)
            //{
            //    return BadRequest("User ID mismatch.");
            //}

            //var updatedUser = _mapper.Map<User>(userDTO);
            //try
            //{
            //    await _userService.UpdateUserAsyncPatch(updatedUser, userToPatch);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Error updating user: {ex.Message}");
            //}

            //return Ok(userToPatch);
        }



        // GET: api/Users/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<ReadUserDTO>> GetUser(int userId, int viewerId) {
            var user = await _userService.GetUserById(userId);
            if(user == null || user.Id != userId) {
            return BadRequest();
            }

            ReadUserDTO userDTO = _mapper.Map<ReadUserDTO>(user);
            userDTO.DisplayingProfile = true;

            if (user.IsProfileHiden) {
                if (user.Id != viewerId) {
                    var projectApplications = await _context.ProjectApplications
                        .Where(pa => pa.ApplicantId == userId)
                        .Include(pa => pa.Project)
                        .ToListAsync();

                    if (!projectApplications.Any(pa => pa.Project.OwnerId == viewerId)) {
                        userDTO.Projects = new List<Models.DTO.Project.ReadProjectNameDTO>();
                        userDTO.Skills = new List<Skill>();
                        userDTO.Description = "This user's profile is set to be hidden";
                        userDTO.DisplayingProfile = false;
                    }
                }
            }
            try {
                return userDTO;
            } catch (UserNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize]
        [HttpPut("{id}/AddSkill")]
        public async Task<ActionResult> AddSkillToUser(int id, string skill) {
            skill = skill.Trim();
            if (skill == null || skill == "") {
                return BadRequest("not a valid skill");
            }

            var user = await _userService.GetUserById(id);
            if (id != user.Id) {
                return NotFound();
            }

            if(user.Skills.Any(s => s.Name.ToLower() == skill.ToLower())) {
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

        [Authorize]
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDTO userDTO) {
            var user = _mapper.Map<User>(userDTO);
            return CreatedAtAction("GetUser", new { id = user.Id }, await _userService.AddUser(user));
        }

        // DELETE: api/Users/5
        [Authorize]
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
