using System.Collections.ObjectModel;

namespace Lagalt_Backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }


        public string RepositoryLink { get; set; } = "";

        //public virtual List<User> Members { get; set; } = new List<User>();
        public virtual List<ProjectApplication> Applications { get; set; } = new List<ProjectApplication>();
        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
