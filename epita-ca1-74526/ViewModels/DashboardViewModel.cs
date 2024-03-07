using epita_ca1_74526.Models;

namespace epita_ca1_74526.ViewModels
{
    public class DashboardViewModel
    {
        public List<AccountBank> AccountsBank { get; set; }
        public List<Transaction> Transactions { get; set; }

        public AppUser user { get; set; }
    }
}
