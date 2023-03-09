using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class Project
    {

        public int ProjectId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string ProjectType { get; set; }
        [Url]
        public string GitHubLink { get; set; }
        [Url]
        public string GitLabLink { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
        public ICollection<ProjectApplication> ProjectApplications { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
