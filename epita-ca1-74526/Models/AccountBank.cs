using epita_ca1_74526.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Models
{
    /// Represents a bank account.
    public class AccountBank
    {
        /// Gets or sets the ID of the account.
        [Key]
        public int Id { get; set; }

        /// Gets or sets the balance of the account.
        public int Balance { get; protected set; } = 0;

        /// Gets or sets the ID of the user associated with the account.
        [ForeignKey("AppUser")]
        public int UserId { get; set; }

        /// Gets or sets the name of the account.
        public string? Name { get; set; }

        /// Gets or sets the type of the account.
        public AccountType accountType { get; set; }

        /// Gets or sets the list of transactions associated with the account.
        public List<Transaction> transactions { get; set; } = new List<Transaction>();

        /// Removes the specified amount from the account balance.
        /// <param name="Amount">The amount to be removed.</param>
        /// <returns>The updated balance of the account.</returns>
        public int removeMoney(int Amount)
        {
            if (Amount > Balance)
            {
                Balance = 0;
            }
            else
            {
                Balance -= Amount;
            }
            return Balance;
        }

        /// Adds the specified amount to the account balance.
        /// <param name="Amount">The amount to be added.</param>
        /// <returns>The updated balance of the account.</returns>
        public int addMoney(int Amount)
        {
            Balance += Amount;
            return Balance;
        }
    }
}
