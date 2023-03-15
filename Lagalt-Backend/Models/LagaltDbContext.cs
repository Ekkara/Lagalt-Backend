using Lagalt_Backend.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagaltDbContext : DbContext
    {
        public LagaltDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectApplication> ProjectApplications { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between User and Skill
            modelBuilder.Entity<User>()
                 .HasMany(u => u.Skills)
                 .WithMany(s => s.Users)
                 .UsingEntity<Dictionary<string, object>>(
                 "UserSkill",
                 u => u.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                 s => s.HasOne<User>().WithMany().HasForeignKey("UserId"),
                joinEntity =>
                {
                    joinEntity.HasKey("UserId", "SkillId");
                    joinEntity.ToTable("UserSkills");

                    joinEntity.Property<int>("UserId");
                    joinEntity.Property<int>("SkillId");

                    joinEntity.HasData(
                        new { UserId = 1, SkillId = 1 },
                        new { UserId = 2, SkillId = 2 },
                        new { UserId = 2, SkillId = 3 }
                    );
                });

            // Configure one-to-many relationship between User and Message
            modelBuilder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-many relationship between User and Project
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationship between Project and Skill
            modelBuilder.Entity<Project>()
                 .HasMany(p => p.Skills)
                 .WithMany(s => s.Projects)
                 .UsingEntity<Dictionary<string, object>>(
                 "ProjectSkill",
                 p => p.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                 s => s.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                joinEntity =>
                {
                    joinEntity.HasKey("ProjectId", "SkillId");
                    joinEntity.ToTable("ProjectSkills");

                    joinEntity.Property<int>("ProjectId");
                    joinEntity.Property<int>("SkillId");

                    joinEntity.HasData(
                        new { ProjectId = 1, SkillId = 1 },
                        new { ProjectId = 2, SkillId = 2 },
                        new { ProjectId = 2, SkillId = 3 }
                    );
                });

            // Configure one-to-many relationship between Project and ProjectApplication
            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectApplications)
                .WithOne(pa => pa.Project)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                 .HasMany(p => p.Applicants)
                 .WithMany(p => p.Projects)
                 .UsingEntity<ProjectApplication>(
                     j => j.ToTable("ProjectApplications")
                 );

            // Configure one-to-many relationship between Project and Message
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Messages)
                .WithOne(m => m.Project)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-one relationship between ProjectApplication and User
            modelBuilder.Entity<ProjectApplication>()
                .HasOne(pa => pa.User)
                .WithMany(u => u.ProjectApplications)
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configure many-to-one relationship between Message and User
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>().HasData(SeedData.GetUser());
            modelBuilder.Entity<Skill>().HasData(SeedData.GetSkill());
            modelBuilder.Entity<Project>().HasData(SeedData.GetProject());
            modelBuilder.Entity<ProjectApplication>().HasData(SeedData.GetProjectApplication());
            modelBuilder.Entity<Message>().HasData(SeedData.GetMessage());


        }
    }
}
