using epita_ca1_74526.Models;

namespace epita_ca1_74526.ViewModels
{
    public class UserDetailViewModel
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<AccountBank> AccountsBank { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
