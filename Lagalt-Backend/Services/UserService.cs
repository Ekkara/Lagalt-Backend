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
    }
}
