using epita_ca1_74526.Models;

namespace epita_ca1_74526.ViewModels
{
    public class CreateTransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public IEnumerable<AccountBank> UserAccounts { get; set; }
    }
}
