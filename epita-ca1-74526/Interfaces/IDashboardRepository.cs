using epita_ca1_74526.Models;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<AccountBank>> GetAllUserAccountsBank();

        Task<List<Transaction>> GetAllUserTransactions();
    }
}
