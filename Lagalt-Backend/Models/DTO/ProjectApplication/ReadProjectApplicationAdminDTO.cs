namespace Lagalt_Backend.Models.DTO.ProjectApplication
{
    public class ReadProjectApplicationAdminDTO
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string? ApplicantName { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
        public string? Status { get; set; }
    }
}
