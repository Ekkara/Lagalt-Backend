using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagaltDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public LagaltDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
