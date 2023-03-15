using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models.Main
{
    public class Project
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? ProjectType { get; set; }

        [Url]
        public string? GitHubLink { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        public List<User>? Applicants { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<ProjectApplication>? ProjectApplications { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
