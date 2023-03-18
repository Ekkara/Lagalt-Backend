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
using Lagalt_Backend.Models.DTO.Project;
using AutoMapper;
using Lagalt_Backend.Models.DTO.ProjectApplication;
using Lagalt_Backend.Models.DTO.Message;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly LagaltDbContext _context;

        public ProjectsController(IUserService userService, IProjectService projectService, IMapper mapper, LagaltDbContext context)
        {
            _userService = userService; //used when adding a user to the project
            _projectService = projectService;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadProjectAdminInfoDTO>>> GetProjects()
        {
            var projects = _mapper.Map<List<ReadProjectAdminInfoDTO>>(await _context.Projects.Include(p => p.Applications).Include(p => p.Messages).ToListAsync());

            return Ok(projects);
            return Ok(await _projectService.GetAllProjects());
        }
        [HttpGet("{id}/AdminProjectView")]
        public async Task<ActionResult<ReadProjectAdminInfoDTO>> GetAdminProjectView(int id) {
            try {
                return Ok(_mapper.Map<ReadProjectAdminInfoDTO>(await _projectService.GetProjectInAdminViewById(id)));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        [HttpGet("ProjectsForMainPage")]
        public async Task<ActionResult<IEnumerable<GetProjectForMainDTO>>> GetMainProjects() {
            var projects = await _context.Projects.ToListAsync();
            var projectDtos = _mapper.Map<List<GetProjectForMainDTO>>(projects);
            return Ok(projectDtos);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProjectDetails>> GetProject(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                return Ok(_mapper.Map<GetProjectDetails>(project));
            }
            catch (ProjectNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProject(int id, EditProjectDTO projectDTO)
        {
            var project = await _projectService.GetProjectById(id);
            if (id != project.Id)
            {
                return BadRequest();
            }
            project.ProjectName = projectDTO.ProjectName;
            project.Description= projectDTO.Description;
            project.CategoryName = projectDTO.CategoryName;
            project.IsAvailable = projectDTO.IsAvailable;

            try
            {
                await _projectService.UpdateProject(project);
            }
            catch (ProjectNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }
        [HttpPut("{projectId}/AddMemberToProject")]
        public async Task<ActionResult> AddMemberToProject(int projectId, int userId) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id || project == null) {
                return BadRequest();
            }

            var user = await _userService.GetUserById(userId);
            if (userId != user.Id || user == null) {
                return BadRequest();
            }

            //project.Members.Add(user);

            try {
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }
        [HttpPut("{projectId}/RemoveMemberToProject")]
        public async Task<ActionResult> RemoveMemberToProject(int projectId, int userId) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id || project == null) {
                return BadRequest("Project does not exist!");
            }

            var user = await _userService.GetUserById(userId);
            if (userId != user.Id || user == null) {
                return BadRequest("User does not exist!");
            }

            //if (!project.Members.Any(member => member.Id == userId)) {
            //    return BadRequest("user was not in the project");
            //}

            //project.Members.Remove(user);

            try {
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
            return Ok();
        }
        [HttpPut("{projectId}/AddProjectApplication")]
        public async Task<ActionResult> AddProjectApplication(int projectId, CreateProjectApplicationDTO applicationDTO) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id) {
                return BadRequest();
            }
            ProjectApplication application = _mapper.Map<ProjectApplication>(applicationDTO);
            //application.ProjectId = projectId;
            application.Project = project;
            project.Applications.Add(application);

            try {
                _context.ProjectApplications.Update(application);
                await _context.SaveChangesAsync();
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }
        [HttpPut("{projectId}/AddMessage")]
        public async Task<ActionResult> AddMessage(int projectId, CreateMessageDTO messageDTO) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id) {
                return BadRequest();
            }
            Message message = _mapper.Map<Message>(messageDTO);
            message.Project = project;

            //find senders name
            var sender = await _context.Users.FindAsync(message.SenderId);
            if (sender == null) {
                return BadRequest("sender is not a existing user");
            }
            message.SenderName = sender.UserName;

            project.Messages.Add(message);

            try {
                _context.Messages.Update(message);
                await _context.SaveChangesAsync();
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }
        [HttpGet("{id}/projectApplication")]
        public async Task<ActionResult<ProjectApplication>> GetProjectApplication(int id) {
            try {
                var application = await _projectService.GetProjectApplicationById(id);
                return Ok(application);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }


        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(CreateProjectDTO projectDTO)
        {
            Project project = _mapper.Map<Project>(projectDTO);
            return CreatedAtAction("GetProject", new { id = project.Id }, await _projectService.AddProject(project));
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteProject(id);
            }
            catch (ProjectNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }
    }
}
