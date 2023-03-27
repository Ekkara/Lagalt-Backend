using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lagalt_Backend.Models
{
    public class Project
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [MaxLength(30)]
        [Required]
        public string ProjectName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(20)]
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        [MaxLength(int.MaxValue)]
        public string? RepositoryLink { get; set; } = "";

        [JsonIgnore]
        public virtual List<User>? Members { get; set; } = new List<User>();
        public virtual List<ProjectApplication>? Applications { get; set; } = new List<ProjectApplication>();
        public virtual List<Message>? Messages { get; set; } = new List<Message>();
    }
}
