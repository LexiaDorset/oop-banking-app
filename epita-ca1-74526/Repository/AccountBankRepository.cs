using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.EntityFrameworkCore;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Repository
{
    public class AccountBankRepository : IAccountBankRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountBankRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(AccountBank account)
        {
            _context.Add(account);
            return Save();
        }

        public bool Delete(AccountBank account)
        {
            _context.Remove(account);
            return Save();
        }

        public async Task<IEnumerable<AccountBank>> GetAll()
        {
            return await _context.AccountsBank.ToListAsync();
            
        }

        public async Task<AccountBank> GetByIdAsync(int id)
        {
            return await _context.AccountsBank.Include(a =>a.transactions).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<AccountBank>> GetByUserIdAsync(int id)
        {
            return await _context.AccountsBank.Include(a => a.transactions).Where(a => a.UserId == id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AccountBank account)
        {
            _context.Update(account);
            return Save();
        }
    }
}
