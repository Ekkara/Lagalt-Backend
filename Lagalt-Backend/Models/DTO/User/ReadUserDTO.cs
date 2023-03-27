using Lagalt_Backend.Models.DTO.Project;

namespace Lagalt_Backend.Models.DTO.User
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string KeycloakId { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; }
        public string Description { get; set; }
        public bool DisplayingProfile { get; set; }

        public List<ReadProjectNameDTO> Projects { get; set; } = new List<ReadProjectNameDTO>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}
