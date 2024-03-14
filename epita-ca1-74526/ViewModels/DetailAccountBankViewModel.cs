using epita_ca1_74526.Models;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.ViewModels
{
    public class DetailAccountBankViewModel
    {
        public AccountBank AccountBank { get; set; }
        public IEnumerable<Transaction> TransactionsAccount { get; set; }
    }
}
