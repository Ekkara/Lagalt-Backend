using System.Text.Json.Serialization;

namespace Lagalt_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsProfileHiden { get; set; }

        [JsonIgnore]
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}
