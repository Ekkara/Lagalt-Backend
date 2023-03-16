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

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly LagaltDbContext _context;

        public ProjectsController(IProjectService projectService, IMapper mapper, LagaltDbContext context)
        {
            _projectService = projectService;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return Ok(await _projectService.GetAllProjects());
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
