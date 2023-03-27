﻿namespace Lagalt_Backend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }

        public string Text { get; set; }
        
        public DateTime PostedTime { get; set; }
    }
}
