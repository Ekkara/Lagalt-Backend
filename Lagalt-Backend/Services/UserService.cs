using Lagalt_Backend.Exceptions;
using Lagalt_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly LagaltDbContext _context;

        public UserService(LagaltDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(user => user.Skills)
                .Include(user => user.Projects)
                .Where(user => user.Id == id)
                .FirstOrDefaultAsync();
            
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }
        public async Task RemoveSkillIfLast(string skill) {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("function called");

            Console.WriteLine(_context.Skills.Count());

            var theSkill = await _context.Skills
                .Where(s => s.Name == skill)
                .FirstOrDefaultAsync();


            Console.WriteLine("loked for skill");

            if (theSkill == null) return;


            Console.WriteLine("skill found");
            var user = _context.Users.FirstOrDefault(user => user.Skills.Any(s => s.Name == skill));
            if (user == null) {
                _context.Skills.Remove(theSkill);
                await _context.SaveChangesAsync();
                Console.WriteLine("Deleted");
            }
            else {
                // User found, print the ID
                Console.WriteLine($"Found user with ID: {user.Id}");
            }
        }
        public async Task<User> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            if (user == null)
            {
                throw new UserNotFoundException(user.Id);
            }
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserAsyncKeycloak(string keycloakId, string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.KeycloakId == keycloakId);

            if (user == null)
            {
                return await PostAsyncKeycloakUsername(keycloakId, username);
            }
            return user;
        }
        public async Task<User> PostAsyncKeycloakUsername(string keycloakId, string username) {
            User user = new User { KeycloakId = keycloakId, UserName = username };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsyncPatch(User updatedUser, User userToPatch)
        {
            if (updatedUser.UserName != null)
            {
                userToPatch.UserName = updatedUser.UserName;
            }
            if (updatedUser.IsProfileHiden != null)
            {
                userToPatch.IsProfileHiden = updatedUser.IsProfileHiden;
            }
            if (updatedUser.Description != null)
            {
                userToPatch.Description = updatedUser.Description;
            }
            if (updatedUser.PictureURL != null)
            {
                userToPatch.PictureURL = updatedUser.PictureURL;
            }
            _context.Entry(userToPatch).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserInDbKeycloak(string keycloakId)
        {
            return await _context.Users.AnyAsync(c => c.KeycloakId == keycloakId);
        }

        public User GetUserFromKeyCloak(string keycloakId)
        {
            User user = _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId).Result;
            return user;
        }
    }
}
