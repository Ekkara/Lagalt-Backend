using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.Message;
using AutoMapper;
using Lagalt_Backend.Services;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;
        private readonly IService<Message> _messageService;

        public MessagesController(/*IService<Message> messageService,*/ IMapper mapper, LagaltDbContext context)
        {
            _context = context;
            _mapper = mapper;
            //_messageService = messageService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessage()
        {
          if (_context.Message == null)
          {
              return NotFound();
          }
            return await _context.Message.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
          if (_context.Message == null)
          {
              return NotFound();
          }
            var message = await _context.Message.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{projectId}")]
        public async Task<ActionResult<Message>> PostMessage(int projectId, CreateMessageDTO messageDTO)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if(project.Id != projectId || project == null) {
                return BadRequest();
            }

            //find senders name
            Message message = _mapper.Map<Message>(messageDTO);
            var sender = await _context.Users.FindAsync(message.SenderId);
            if(sender == null) { 
            return BadRequest("sender is not a existing user");
            }
            message.SenderName = sender.UserName;
            
            //_context.Message.Add(message);
            project.Messages.Add(message);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (_context.Message == null)
            {
                return NotFound();
            }
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return (_context.Message?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
