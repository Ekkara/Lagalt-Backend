using Lagalt_Backend.Models;

namespace Lagalt_Backend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
    }
}
