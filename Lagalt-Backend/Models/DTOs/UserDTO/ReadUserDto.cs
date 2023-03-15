using Lagalt_Backend.Models.Main;

namespace Lagalt_Backend.Models.DTOs.UserDTO
{
    public class ReadUserDto
    {
        public int Id { get; set; }
        public string? KeycloakId { get; set; }
        public string? UserName { get; set; }
        public string? Picture { get; set; }
        public string? Status { get; set; }

        public string? Description { get; set; }
        public bool IsProfileHidden { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public ICollection<ProjectApplication>? ProjectApplications { get; set; }
    }
}
