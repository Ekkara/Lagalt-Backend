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
using Lagalt_Backend.Helpers;

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
        public async Task<ActionResult<IEnumerable<ReadProjectAdminInfoDTO>>> GetProjects() //this is a not a necisary call! i can't see any use for getting all project as admin
        {
            var projects = _mapper.Map<List<ReadProjectAdminInfoDTO>>(await _context.Projects.Include(p => p.Applications).Include(p => p.Messages).ToListAsync());
            return Ok(projects);
        }
        [HttpGet("{id}/ProjectExist")] // no keycloak needed
        public async Task<ActionResult<bool>> ReadIfProjectExist(int id) {
            try {
                return Ok(await _context.Projects.AnyAsync(project => project.Id == id));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> GetUserRelationToProject(int projectId) {
            var keycloakId = User.GetId();
            var username = User.GetUsername();

            if (string.IsNullOrEmpty(keycloakId) || string.IsNullOrEmpty(username)) {
                return NotFound();
            }
            var user = await _userService.GetUserAsyncKeycloak(keycloakId, username);
            if (user == null) {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(projectId);
            if(project == null) {
                return NotFound();
            }

            if (project.OwnerId == user.Id) return Ok(3); //user is admin => admin view
            if(project.Members.Any(member => member.Id == user.Id)) return Ok(2); //admin is collabirator => see colabirator view

            return Ok(1); //neither argument is true, so the user must be logged in but not related to the project thus they see the logged in view
        }
        [HttpGet("{id}/AdminProjectView")] //keycloak needed to verify they are admin
        public async Task<ActionResult<ReadProjectAdminInfoDTO>> GetAdminProjectView(int id) {
            var keycloakId = User.GetId();
            var username = User.GetUsername();

            if (string.IsNullOrEmpty(keycloakId) || string.IsNullOrEmpty(username)) {
                return NotFound();
            }
            var user = await _userService.GetUserAsyncKeycloak(keycloakId, username);
            if (user == null) {
                return NotFound();
            }
            var project = await _projectService.GetProjectInAdminViewById(id);
            if (project.OwnerId != user.Id) return BadRequest("admin rights are needed to view admin page");

            try {
                return Ok(_mapper.Map<ReadProjectAdminInfoDTO>(project));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        [HttpGet("{id}/CollaboratorProjectView")] //keycloak needed to verify they are collaborator
        public async Task<ActionResult<ReadProjectCollaboratorInfoDTO>> GetCollaboratorProjectView(int id) {
            var keycloakId = User.GetId();
            var username = User.GetUsername();

            if (string.IsNullOrEmpty(keycloakId) || string.IsNullOrEmpty(username)) {
                return NotFound();
            }
            var user = await _userService.GetUserAsyncKeycloak(keycloakId, username);
            if (user == null) {
                return NotFound();
            }
            var project = await _projectService.GetProjectInAdminViewById(id);
            if (!project.Members.Any(u => u.Id == user.Id)) return BadRequest("collabirator role are needed to see the collaborator view");
            try {
                return Ok(_mapper.Map<ReadProjectCollaboratorInfoDTO>(project));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }
        [HttpGet("{id}/NonCollaboratorProjectView")] //keycloak is not needed as no private data is shown, and the data should be accasible to not logged in members too
        public async Task<ActionResult<ReadProjectNonCollaboratorInfoDTO>> NonCollaboratorProjectView(int id) {
            try {
                return Ok(_mapper.Map<ReadProjectNonCollaboratorInfoDTO>(await _projectService.GetProjectById(id)));
            } catch (ProjectNotFoundException ex) {
                return NotFound(new ProblemDetails {
                    Detail = ex.Message,
                });
            }
        }

        [HttpGet("ProjectsForMainPage")] //no keycloak required
        public async Task<ActionResult<IEnumerable<ReadProjectNameDTO>>> GetMainProjects(int start, int range) {
            var projects = await _context.Projects
                //.Where(project => searchCatagoryType.Contains(project.CategoryName))
                .ToListAsync();

            if (start < 0 || range < 0) {

                return BadRequest("Invalid start or range values");
            }
            if(start >= projects.Count) {
                return Ok();
            }

            int end = start + range - 1;

            if (end >= projects.Count) {
                end = projects.Count - 1;
            }
            return Ok(_mapper.Map<List<ReadProjectNameDTO>>(projects).GetRange(start, end - start + 1));
        }

        // GET: api/Projects/5
        [HttpGet("{id}")] //this should never be used, thus it should be deleted
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
        [HttpPut("{id}")] //keycloak could be used to verify they are logged in, but the user wont access any private data if it is not used
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
            project.RepositoryLink = projectDTO.RepositoryLink;

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
        [HttpPut("{projectId}/RemoveMemberToProject")]//only accessed on backend, do we need keycloak here?
        public async Task<ActionResult> RemoveMemberToProject(int projectId, int userId) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id || project == null) {
                return BadRequest("Project does not exist!");
            }

            var user = await _userService.GetUserById(userId);
            if (userId != user.Id || user == null) {
                return BadRequest("User does not exist!");
            }

            if (!project.Members.Any(member => member.Id == userId)) {
                return BadRequest("user was not in the project");
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

        [HttpPut("{projectId}/AddProjectApplication")] //could use keycloak to verify that the user exist i guess
        public async Task<ActionResult> AddProjectApplication(int projectId, CreateProjectApplicationDTO applicationDTO) {
            var project = await _projectService.GetProjectById(projectId);
            if (projectId != project.Id) {
                return BadRequest();
            }

            var sender = await _userService.GetUserById(applicationDTO.ApplicantId);
            if(sender == null || sender.Id != applicationDTO.ApplicantId) {
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
        [HttpPut("{applicationId}/RemoveProjectApplicationFromProject")]  //could use keycloak to verify that the user exist i guess
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
        [HttpPut("{applicationId}/AcceptProjectApplication")] //could use keycloak to verify that the user exist i guess
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
            var user = await _userService.GetUserById(projectDTO.OwnerId);
            if(user == null || projectDTO.OwnerId != user.Id) {
            return BadRequest("User to set as the owner is not found");
            }

            Project project = _mapper.Map<Project>(projectDTO);
            project.Members.Add(user);
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
