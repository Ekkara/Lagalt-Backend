using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }

        [Required]
        [StringLength(1000)]
        public string MessageText { get; set; }
    }
}
