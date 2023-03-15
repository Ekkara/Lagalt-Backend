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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "Maddie", Hidden = true},
                new User { Id = 2, UserName = "Alice", Hidden = false}
                );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, ProjectName = "Final Fantasy XIV", Description = "World-Class MMORPG", CategoryName = "Games", IsAvailable = true },
                new Project { Id = 2, ProjectName = "Scream", Description = "Rock song doubling as a boss theme", CategoryName = "Music", IsAvailable = false }
                );
        }

    }
}
