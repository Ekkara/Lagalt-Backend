using Lagalt_Backend.Models.DTO.Message;
using Lagalt_Backend.Models.DTO.ProjectApplication;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Lagalt_Backend.Models.DTO.Project
{
    public class ReadProjectAdminInfoDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }

        public string RepositoryLink { get; set; }
        public virtual List<ReadProjectApplicationDTO> Applications { get; set; } = new List<ReadProjectApplicationDTO>();
        public virtual List<ReadMessageDTO> Messages { get; set; } = new List<ReadMessageDTO>();
    }
}
