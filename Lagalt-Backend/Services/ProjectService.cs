using Lagalt_Backend.Models;
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
            return await _context.Projects.ToListAsync();
        }
    }
}
