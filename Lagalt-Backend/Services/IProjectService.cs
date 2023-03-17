using Lagalt_Backend.Models;

namespace Lagalt_Backend.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectInAdminViewById(int id);
        Task<Project> GetProjectById(int id);
        Task<ProjectApplication> GetProjectApplicationById(int id);
        Task<Project> UpdateProject(Project project);
        Task<Project> AddProject(Project project);
        Task DeleteProject(int id);
    }
}
