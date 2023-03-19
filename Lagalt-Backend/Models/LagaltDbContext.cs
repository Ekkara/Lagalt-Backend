using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;

namespace Lagalt_Backend.Models
{
    public class LagaltDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectApplication> ProjectApplications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public LagaltDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Project>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<User>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<ProjectApplication>()
                .HasKey(od => od.Id);

            modelBuilder.Entity<ProjectApplication>()
                .HasOne(od => od.Project)
                .WithMany(o => o.Applications)
                .HasForeignKey(od => od.ProjectId);


            modelBuilder.Entity<Message>()
        .HasKey(od => od.Id);

            modelBuilder.Entity<Message>()
                .HasOne(od => od.Project)
                .WithMany(o => o.Messages)
                .HasForeignKey(od => od.ProjectId);

             modelBuilder.Entity<User>()
                 .HasMany<Project>(user => user.Projects)
                 .WithMany(project => project.Members)
                 .UsingEntity<Dictionary<string, object>>(
                 "UserProject",
                 u => u.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                 s => s.HasOne<User>().WithMany().HasForeignKey("UserId"),
                joinEntity =>
                {
                    joinEntity.HasKey("UserId", "ProjectId");
                    joinEntity.ToTable("UserProject");

                    joinEntity.Property<int>("UserId");
                    joinEntity.Property<int>("ProjectId");
                });

            modelBuilder.Entity<Skill>()
                .HasKey(od => od.Id);


            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "Maddie", IsProfileHiden = true },
                new User { Id = 2, UserName = "Alice", IsProfileHiden = false }
                );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, ProjectName = "Final Fantasy XIV", Description = "World-Class MMORPG", CategoryName = "Games", IsAvailable = true },
                new Project { Id = 2, ProjectName = "Scream", Description = "Rock song doubling as a boss theme", CategoryName = "Music", IsAvailable = false }
                );
        }
    }
}
