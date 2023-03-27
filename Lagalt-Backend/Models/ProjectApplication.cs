using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class ProjectApplication
    {
        [Required]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int ApplicantId { get; set; }
        public string? ApplicantName { get; set; }
        [MaxLength(200)]
        public string? Message { get; set; }
        [Required]
        public string? Date { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
    }
}
