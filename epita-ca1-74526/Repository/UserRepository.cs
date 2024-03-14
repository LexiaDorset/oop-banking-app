using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersUser()
        {
            // Charge tous les utilisateurs
            var allUsers = await _context.Users.ToListAsync();

            // Filtrer les utilisateurs ayant le rôle "User"
            var usersWithUserRole = allUsers.Where(u => _userManager.IsInRoleAsync(u, "User").Result);

            return usersWithUserRole;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
