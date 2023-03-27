using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lagalt_Backend.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string KeycloakId { get; set; }
        [MaxLength(40)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(int.MaxValue)]
        public string? PictureURL { get; set; } = "";
        [MaxLength(500)]
        public string? Description { get; set; } = "";
        [Required]
        public bool IsProfileHiden { get; set; }

        [JsonIgnore]
        public List<Project>? Projects { get; set; } = new List<Project>();
        public List<Skill>? Skills { get; set; } = new List<Skill>();
    }
}
