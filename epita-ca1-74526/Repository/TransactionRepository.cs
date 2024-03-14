using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.EntityFrameworkCore;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Transaction transaction)
        {
            _context.Add(transaction);
            return Save();
        }

        public bool Delete(Transaction transaction)
        {
            _context.Remove(transaction);
            return Save();
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _context.Transactions.ToListAsync();

        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int id)
        {
            return await _context.Transactions.Where(i => i.AccountId == id).ToListAsync();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Transaction transaction)
        {
            _context.Update(transaction);
            return Save();
        }
    }
}
