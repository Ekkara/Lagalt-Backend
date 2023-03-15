﻿using System.ComponentModel.DataAnnotations;

namespace Lagalt_Backend.Models.Main
{
    public class ProjectApplication
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(50)]
        public string? Status { get; set; }
    }
}