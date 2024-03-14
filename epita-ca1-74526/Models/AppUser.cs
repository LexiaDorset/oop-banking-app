using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Models
{
    /// Represents an user.
    public class AppUser : IdentityUser<int>
    {

        /// Gets or sets the first name of the user.
        public string? firstName { get; set; }

        /// Gets or sets the last name of the user.
        public string? lastName { get; set; }

        /// Gets or sets the PIN of the user.
        public string? pin { get; set; }

        /// Gets or sets the balance of the user.
        public int? Balance { get; set; } = 0;

        /// Adds the specified amount to the user's balance.
        /// <param name="amount">The amount to add.</param>
        public void addMoney(int amount)
        {
            Balance += amount;
        }

        /// Removes the specified amount from the user's balance.
        /// <param name="amount">The amount to remove.</param>
        public void removeMoney(int amount)
        {
            Balance -= amount;
        }

    }
}
