namespace Lagalt_Backend.Models
{
    public class UserSkill
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int SkillId { get; set; }
        public Skill Skills { get; set; }
    }
}
