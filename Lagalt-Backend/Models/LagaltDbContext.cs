using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagaltDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<ProjectApplication> ProjectApplications { get; set; }
        public DbSet<Message> Messages { get; set; }

        public LagaltDbContext(DbContextOptions options) : base(options)
        {

        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>()
           .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skills)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId);

            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillSetId });

            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);

            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillSetId);

            modelBuilder.Entity<ProjectApplication>()
                .HasKey(pa => pa.ApplicationId);

            modelBuilder.Entity<ProjectApplication>()
                .HasOne(pa => pa.Project)
                .WithMany(p => p.ProjectApplications)
                .HasForeignKey(pa => pa.ProjectId);

            modelBuilder.Entity<ProjectApplication>()
                .HasOne(pa => pa.User)
                .WithMany(u => u.ProjectApplications)
                .HasForeignKey(pa => pa.UserId);

            modelBuilder.Entity<Message>()
                .HasKey(m => m.MessageId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Project)
                .WithMany(p => p.Messages)
                .HasForeignKey(m => m.ProjectId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>().HasData(SeedData.GetUser());
            modelBuilder.Entity<Skill>().HasData(SeedData.GetSkill());
            modelBuilder.Entity<Project>().HasData(SeedData.GetProject());
            modelBuilder.Entity<UserSkill>().HasData(SeedData.GetUserSkill());
            modelBuilder.Entity<ProjectSkill>().HasData(SeedData.GetProjectSkill());
            modelBuilder.Entity<ProjectApplication>().HasData(SeedData.GetProjectApplication());
            modelBuilder.Entity<Message>().HasData(SeedData.GetMessage());

        }
    }
}
