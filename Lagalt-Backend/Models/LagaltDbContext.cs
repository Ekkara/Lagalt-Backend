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
                new Project { Id = 1, ProjectName = "Final Fantasy XIV", ProjectDescription = "World-Class MMORPG", ProjectCategoryId = 1, ProjectCategoryName = "Games", ProjectIsAvailable = true },
                new Project { Id = 2, ProjectName = "Scream", ProjectDescription = "Rock song doubling as a boss theme", ProjectCategoryId = 2, ProjectCategoryName = "Music", ProjectIsAvailable = false }
                );
        }

    }
}
