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

        Task<User> GetUserAsyncKeycloak(string keycloakId, string username);
        Task UpdateUserAsyncPatch(User updatedUser, User userToPatch);
        Task<User> PostAsyncKeycloakUsername(string keycloakId, string username);
        Task<bool> UserInDbKeycloak(string keycloakId);
        public User GetUserFromKeyCloak(string keycloakId);
    }
}
