using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace epita_ca1_74526.ViewModels
{
    /// View model for the registration form.
    public class RegisterViewModel
    {
        /// Gets or sets the email address.
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        /// Gets or sets the name of the account.
        [HiddenInput]
        public string? NameAccount { get; set; }

        /// Gets or sets the PIN.
        [HiddenInput]
        public string? pin { get; set; }

        /// Gets or sets the first name.
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }

        /// Gets or sets the last name.
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        /// Creates the PIN and account name based on the first and last name.
        public void createPinAndName()
        {
            string y = "" + ((int)char.ToLower(firstName[0]) - 96);
            string z = "" + ((int)char.ToLower(lastName[0]) - 96);
            pin = y + z;
            NameAccount = "" + char.ToLower(firstName[0]) + "" + char.ToLower(lastName[0])
                + "" + "-" + (firstName.Length + lastName.Length) + "-" + y + "-" + z;
        }

        /// Creates a new bank account for the user.
        /// <param name="accountType">The type of the account.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The newly created bank account.</returns>
        public AccountBank createAccountsBank(AccountType accountType, int userId)
        {
            return new AccountBank()
            {
                Name = NameAccount + "-" + accountType.ToString()[0],
                accountType = accountType,
                UserId = userId,
            };
        }
    }
}
