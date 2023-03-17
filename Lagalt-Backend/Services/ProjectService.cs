using Lagalt_Backend.Exceptions;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.Project;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly LagaltDbContext _context;

        public ProjectService(LagaltDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
           

            //await _context.Projects.Include(p => p.Applications)
            //                          .FirstOrDefaultAsync(p => p.Id == id);
            return await _context.Projects.ToListAsync();
        }
        public async Task<Project> GetProjectInAdminViewById(int id) {
            var project = await _context.Projects.Include(p => p.Applications).Include(p => p.Messages).FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<Project> GetProjectById(int id)
        {
           // var application = await _context.ProjectApplications.ToListAsync();
            var project = await _context.Projects.FindAsync(id);
            
            

            if (project == null)
            {
                throw new ProjectNotFoundException(id);
            }

            return project;
        }
        public async Task<ProjectApplication> GetProjectApplicationById(int id) {
            var application = await _context.ProjectApplications.FindAsync(id);

            if (application == null) {
                throw new ProjectNotFoundException(id);
            }

            return application;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            if (project == null)
            {
                throw new ProjectNotFoundException(project.Id);
            }

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                throw new ProjectNotFoundException(id);
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
