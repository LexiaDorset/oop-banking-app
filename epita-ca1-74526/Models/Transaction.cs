using epita_ca1_74526.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Models
{
    /// Represents a transaction.
    public class Transaction
    {
        /// Gets or sets the ID of the transaction.
        [Key]
        public int Id { get; set; }

        /// Gets or sets the date of the transaction.
        public DateTime Date { get; set; }

        /// Gets or sets the amount of the transaction.
        public int Amount { get; set; }

        /// Gets or sets the balance after the transaction.
        public int? Balance { get; set; }

        /// Gets or sets the ID of the associated account.
        [ForeignKey("AccountBank")]
        public int? AccountId { get; set; }

        /// Gets or sets the ID of the associated user.
        [ForeignKey("AppUser")]
        public int? UserId { get; set; }

        /// Gets or sets the title of the transaction.
        public string? Title { get; set; }

        /// Gets or sets the type of the transaction.
        public TransactionType transactionType { get; set; }

        /// Processes the transaction by updating the account and user balances.
        /// <param name="account">The account associated with the transaction.</param>
        /// <param name="user">The user associated with the transaction.</param>
        public void Process(AccountBank account, AppUser user)
        {
            if (transactionType is TransactionType.Withdraw)
            {
                if (Amount > account.Balance)
                {
                    Amount = account.Balance;
                }
                Balance = account.removeMoney(Amount);
                user.removeMoney(Amount);
            }
            else
            {
                Balance = account.addMoney(Amount);
                user.addMoney(Amount);
            }
        }

        /// Initializes a new instance of the <see cref="Transaction"/> class.
        public Transaction()
        {
            Date = DateTime.Now;
        }
    }
}
