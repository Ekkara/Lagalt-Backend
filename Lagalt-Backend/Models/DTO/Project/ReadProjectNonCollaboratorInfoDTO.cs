using Lagalt_Backend.Models.DTO.Message;
using Lagalt_Backend.Models.DTO.User;

namespace Lagalt_Backend.Models.DTO.Project
{
    public class ReadProjectNonCollaboratorInfoDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }

        public string RepositoryLink { get; set; } = "";
        public List<ReadUserNamesDTO> Members { get; set; } = new List<ReadUserNamesDTO>();
    }
}
