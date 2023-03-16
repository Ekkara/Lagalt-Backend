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


        public virtual List<ProjectApplication> Applications { get; set; } = new List<ProjectApplication>();
        public string? DummyData1 { get; set; }
        public string? DummyData2 { get; set; }
        public string? DummyData3 { get; set; }
    }
}
