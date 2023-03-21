namespace Lagalt_Backend.Models.DTO.Project
{
    public class EditProjectDTO
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string RepositoryLink { get; set; }
        public bool IsAvailable { get; set; }

    }
}
