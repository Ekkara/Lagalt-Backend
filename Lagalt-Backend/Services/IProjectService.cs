using Lagalt_Backend.Models;

namespace Lagalt_Backend.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
    }
}
