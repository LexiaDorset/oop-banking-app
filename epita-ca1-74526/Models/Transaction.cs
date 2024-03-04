using epita_ca1_74526.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace epita_ca1_74526.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int? Balance { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey("AppUser")]
        public int? UserId { get; set; }

        public string? Title { get; set; }
        public TransactionType transactionType { get; set; }
        public void Process(AccountBank account)
        {
            if(transactionType is TransactionType.Withdraw)
            {
                Balance = account.removeMoney(Amount);
            }
            else
            {
                Balance = account.addMoney(Amount);
            }
        }
        public Transaction()
        {
            Date = DateTime.Now;
        }

    }
}
