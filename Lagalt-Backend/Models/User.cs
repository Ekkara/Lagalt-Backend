using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password{ get; set; }
        public bool IsProfileHidden { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<ProjectApplication> ProjectApplications { get; set; }
    }
}
