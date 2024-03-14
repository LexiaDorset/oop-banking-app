using epita_ca1_74526.Models;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Interfaces
{
    public interface IAccountBankRepository
    {
        Task<IEnumerable<AccountBank>> GetAll();
        Task<AccountBank> GetByIdAsync(int id);
        Task<IEnumerable<AccountBank>> GetByUserIdAsync(int id);

        // Task<IEnumerable<Account>
        bool Add(AccountBank account);
        bool Update(AccountBank account);  
        bool Delete(AccountBank account);
        bool Save();
    }
}
