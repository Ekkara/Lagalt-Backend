namespace Lagalt_Backend.Models
{
    public class ProjectSkill
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int SkillSetId { get; set; }
        public Skill Skill { get; set; }
    }
}
