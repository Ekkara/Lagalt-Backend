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
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProjectsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly LagaltDbContext _context;

        public ProjectsController(IUserService userService, IProjectService projectService, IMapper mapper, LagaltDbContext context) {
            _userService = userService; //used when adding a user to the project
            _projectService = projectService;
            _mapper = mapper;
            _context = context;
        }
        //[Authorize]
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadProjectAdminInfoDTO>>> GetProjects() {
            var projects = _mapper.Map<List<ReadProjectAdminInfoDTO>>(await _context.Projects.Include(p => p.Applications).Include(p => p.Messages).ToListAsync());
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return project;
        }


        [HttpGet("{id}/ProjectExist")]
        public async Task<ActionResult<bool>> ReadIfProjectExist(int id) {
            try {
                return Ok(await _context.Projects.AnyAsync(project => project.Id == id));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        /// <summary>
        /// function to determain the relationship a user has to the project
        /// return 1 if logged in but not a member, so no relation
        /// return 2 if member to the project
        /// return 3 if owner of the project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("UsersRelationToProject")]
        public async Task<ActionResult<int>> UsersRelationToProject(int projectId, int userId) {
            //verif the user exist
            var user = await _userService.GetUserById(userId);
            if (user == null || user.Id != userId) {
                return NotFound("user not found with the id " + userId);
            }

            //verify the project exist, and store it to look at members 
            var project = await _projectService.GetProjectById(projectId);
            if (project == null || project.Id != projectId) {
                return NotFound("user not found with the id " + userId);
            }

            try {
                if (userId == project.OwnerId) return 3;
                else if (project.Members.Any(member => member.Id == userId)) return 2;
                else return 1;

            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        //[Authorize]
        [HttpGet("{projectId}/AdminProjectView")]
        public async Task<ActionResult<ReadProjectAdminInfoDTO>> GetAdminProjectView(int projectId, int userId) {
            var user = await _userService.GetUserById(userId);
            if (user == null || user.Id != userId) return NotFound("The user was not found");
            
            var project = await _projectService.GetProjectById(projectId);
            if (project == null || project.Id != projectId) return NotFound("The project was not found");
            
            if(project.OwnerId != userId) return Unauthorized("The user do not have acces to view this material"); 

            try {
                return Ok(_mapper.Map<ReadProjectAdminInfoDTO>(await _projectService.GetProjectInAdminViewById(projectId)));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        //[Authorize]
        [HttpGet("{projectId}/CollaboratorProjectView")]
        public async Task<ActionResult<ReadProjectCollaboratorInfoDTO>> GetCollaboratorProjectView(int projectId, int userId) {
            var user = await _userService.GetUserById(userId);
            if (user == null || user.Id != userId) return NotFound("The user was not found");
            
            var project = await _projectService.GetProjectById(projectId);
            if (project == null || project.Id != projectId) return NotFound("The project was not found");
            
            if (!project.Members.Any(member => member.Id == userId)) return Unauthorized("The user do not have acces to view this material");

            try {
                return Ok(_mapper.Map<ReadProjectCollaboratorInfoDTO>(await _projectService.GetProjectInCollaboratorViewById(projectId)));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        [HttpGet("{projectId}/NonCollaboratorProjectView")]
        public async Task<ActionResult<ReadProjectNonCollaboratorInfoDTO>> NonCollaboratorProjectView(int projectId) {
            //public data, no security needed
            var project = await _projectService.GetProjectById(projectId);
            if (project == null || project.Id != projectId) return NotFound("The project was not found");

            try {
                return Ok(_mapper.Map<ReadProjectNonCollaboratorInfoDTO>(await _projectService.GetProjectById(projectId)));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        [HttpGet("ProjectsForMainPage")]
        public async Task<ActionResult<IEnumerable<GetProjectForMainDTO>>> GetMainProjects(int start, int range) {
            var projects = await _context.Projects
                //.Where(project => searchCatagoryType.Contains(project.CategoryName))
                .ToListAsync();

            if (start < 0 || range < 0) {

                return BadRequest("Invalid start or range values");
            }
            if (start >= projects.Count) {
                return Ok();
            }

            int end = start + range - 1;

            if (end >= projects.Count) {
                end = projects.Count - 1;
            }
            return Ok(_mapper.Map<List<GetProjectForMainDTO>>(projects).GetRange(start, end - start + 1));
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProject(int id, EditProjectDTO projectDTO) {
            var project = await _projectService.GetProjectById(id);
            if (id != project.Id) {
                return BadRequest();
            }
            project.ProjectName = projectDTO.ProjectName;
            project.Description = projectDTO.Description;
            project.CategoryName = projectDTO.CategoryName;
            project.IsAvailable = projectDTO.IsAvailable;
            project.RepositoryLink = projectDTO.RepositoryLink;

            try {
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }

        //[Authorize]
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

            project.Members.Add(user);

            try {
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }

        //[Authorize]
        [HttpPut("{projectId}/RemoveMemberFromProject")]
        public async Task<ActionResult> RemoveMemberFromProject(int projectId, int userId) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id || project == null) {
                return BadRequest("Project does not exist!");
            }

            var user = await _userService.GetUserById(userId);
            if (userId != user.Id || user == null) {
                return BadRequest("User does not exist!");
            }

            if (!project.Members.Any(member => member.Id == userId)) {
                return BadRequest("User was not in the project");
            }

            if (project.OwnerId == userId) {
                return BadRequest("Can't kick the owner");
            }

            project.Members.Remove(user);

            try {
                await _projectService.UpdateProject(project);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
            return Ok();
        }

        //[Authorize]
        [HttpPut("{projectId}/AddProjectApplication")]
        public async Task<ActionResult> AddProjectApplication(int projectId, CreateProjectApplicationDTO applicationDTO) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id) {
                return BadRequest();
            }

            var sender = await _userService.GetUserById(applicationDTO.ApplicantId);
            if (sender == null || sender.Id != applicationDTO.ApplicantId) {
                return BadRequest();
            }

            ProjectApplication application = _mapper.Map<ProjectApplication>(applicationDTO);
            //application.ProjectId = projectId;
            application.Project = project;
            application.ApplicantName = sender.UserName;
            application.Date = DateTime.Now.ToShortDateString();

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

        //[Authorize]
        [HttpPut("{applicationId}/RemoveProjectApplicationFromProject")]
        public async Task<ActionResult> RemoveProjectApplicationFromProject(int applicationId) {
            var application = await _context.ProjectApplications.FirstOrDefaultAsync();// .FindAsync(applicationId);

            if (application == null || application.Id != applicationId) {
                return BadRequest();
            }


            var project = await _projectService.GetProjectById(application.ProjectId);
            if (project == null || project.Id != application.ProjectId) {
                return BadRequest();
            }

            application.Status = "Denied";
            project.Applications.Remove(application);

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

        //[Authorize]
        [HttpPut("{applicationId}/AcceptProjectApplication")]
        public async Task<ActionResult> AcceptProjectApplication(int applicationId) {
            var application = await _context.ProjectApplications.FindAsync(applicationId);

            if (application == null || application.Id != applicationId) {
                return BadRequest();
            }

            try {
                await AddMemberToProject(application.ProjectId, application.ApplicantId);
                _context.ProjectApplications.Remove(application);
                await _context.SaveChangesAsync();
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return Ok();
        }

        //[Authorize]
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

        //[Authorize]
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
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(CreateProjectDTO projectDTO) {
            var user = await _userService.GetUserById(projectDTO.OwnerId);
            if (user == null || projectDTO.OwnerId != user.Id) {
                return BadRequest("User to set as the owner is not found");
            }

            Project project = _mapper.Map<Project>(projectDTO);
            project.Members.Add(user);
            return CreatedAtAction("GetProject", new { id = project.Id }, await _projectService.AddProject(project));
        }

        // DELETE: api/Projects/5
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id) {
            try {
                await _projectService.DeleteProject(id);
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }
    }
}
