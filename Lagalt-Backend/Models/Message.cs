using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
        

    }
}
