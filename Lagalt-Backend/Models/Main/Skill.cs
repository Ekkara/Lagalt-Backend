using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models.Main
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? SkillName { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}
