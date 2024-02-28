using epita_ca1_74526.Models;

namespace epita_ca1_74526.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetByIdAsync(int id);
        // Task<IEnumerable<Account>
        bool Add(Account account);
        bool Update(Account account);  
        bool Delete(Account account);
        bool Save();
    }
}
