using epita_ca1_74526.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace epita_ca1_74526.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public int Balance { get; protected set; } = 0;
        public int UserId { get; set; }

        public string? Name { get; set; }
        public AccountType accountType { get; set; }

        public List<Transaction> transactions { get; set; } = new List<Transaction>();

        public int removeMoney(int Amount)
        {
            if(Amount > Balance)
            {
                Balance = 0;
            }
            else
            {
                Balance -= Amount;
            }
            return Balance;
        }

        public int addMoney(int Amount)
        {
            Balance += Amount;
            return Balance;
        }
    }
}
