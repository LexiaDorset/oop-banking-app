using epita_ca1_74526.Models;

namespace epita_ca1_74526.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAll();
        Task<Transaction> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int id);

        // Task<IEnumerable<Account>
        bool Add(Transaction account);
        bool Update(Transaction account);
        bool Delete(Transaction account);
        bool Save();
    }
}
