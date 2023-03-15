using Lagalt_Backend.Models.Main;

namespace Lagalt_Backend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(User user);
        Task<User> AddUser(User user);
        Task DeleteUser(int id);
    }
}
