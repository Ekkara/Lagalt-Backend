using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class Skill
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(40)]
        [Required]
        public string? Name { get; set; } = "";
    }
}
