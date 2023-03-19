using Lagalt_Backend.Models;

namespace Lagalt_Backend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task RemoveSkillIfLast(string skill);
        Task<User> UpdateUser(User user);
        Task<User> AddUser(User user);
        Task DeleteUser(int id);
    }
}
