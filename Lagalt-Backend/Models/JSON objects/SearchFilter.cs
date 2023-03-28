namespace Lagalt_Backend.Models.JSON_objects
{
    public class SearchFilter
    {
        public string SearchString { get; set; }
        public List<string> CategoryFilter { get; set; }
        public bool ShowClosedProject { get; set; }
    }
}
