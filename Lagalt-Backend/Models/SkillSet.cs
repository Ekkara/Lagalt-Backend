using NuGet.Protocol;

namespace Lagalt_Backend.Models
{
    public class SkillSet
    {
        public int Id { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}
