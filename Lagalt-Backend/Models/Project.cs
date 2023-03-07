namespace Lagalt_Backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int ProjectCategoryId { get; set; }
        public string ProjectCategoryName { get; set; }
    }
}
