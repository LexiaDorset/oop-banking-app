using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using System.Security.Claims;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<AccountBank>> GetAllUserAccountsBank()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userAccountsBank = _context.AccountsBank.Where(r => r.UserId == int.Parse(curUser.FindFirstValue(ClaimTypes.NameIdentifier)));
            return userAccountsBank.ToList();
        }
        public async Task<List<Transaction>> GetAllUserTransactions()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userTransactions = _context.Transactions.Where(r => r.UserId == int.Parse(curUser.FindFirstValue(ClaimTypes.NameIdentifier)));
            return userTransactions.ToList();
        }
    }
}
