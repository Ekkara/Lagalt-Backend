
namespace Lagalt_Backend.Models.DTO.Project
{
    public class GetProjectDetails
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
