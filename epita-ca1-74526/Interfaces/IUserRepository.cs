using epita_ca1_74526.Models;

namespace epita_ca1_74526.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<IEnumerable<AppUser>> GetAllUsersUser();

        Task<AppUser> GetUserById(int id);
        bool Add(AppUser user);
        bool Update(AppUser user); 
        bool Delete(AppUser user);
        bool Save();
    }
}
