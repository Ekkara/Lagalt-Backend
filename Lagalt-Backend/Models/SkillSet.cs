using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class SkillSet
    {
        [Required]
        public int Id { get; set; }
        public List<Skill>? Skills { get; set; } = new List<Skill>();
    }
}
