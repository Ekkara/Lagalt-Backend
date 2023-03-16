namespace Lagalt_Backend.Models
{
    public class ProjectApplication
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int ApplicantId { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
    }
}
