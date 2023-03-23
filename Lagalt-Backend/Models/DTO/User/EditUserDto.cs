namespace Lagalt_Backend.Models.DTO.User
{
    public class EditUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsProfileHiden { get; set; }
    }
}
