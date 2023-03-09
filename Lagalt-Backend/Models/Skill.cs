using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required]
        [StringLength(50)]
        public string SkillName { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
    }
}
