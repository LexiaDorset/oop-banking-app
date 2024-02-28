using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Account account)
        {
            _context.Add(account);
            return Save();
        }

        public bool Delete(Account account)
        {
            _context.Remove(account);
            return Save();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
            
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.Include(a =>a.transactions).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Account account)
        {
            _context.Update(account);
            return Save();
        }
    }
}
