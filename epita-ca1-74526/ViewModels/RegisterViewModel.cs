using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace epita_ca1_74526.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [HiddenInput]
        public string? NameAccount { get; set; }

        [HiddenInput]
        public string? pin { get; set; }

        /* [Required]
         [DataType(DataType.Password)]
         public string Password { get; set; }
         [Display(Name = "Confirm password")]
         [Required(ErrorMessage = "Confirm password is required")]
         [Compare("Password", ErrorMessage = "Password do not match")]
         public string ConfirmPassword { get; set; }*/

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        public void createPinAndName()
        {
            string y = "" + ((int)char.ToLower(firstName[0]) - 96);
            string z = "" + ((int)char.ToLower(lastName[0]) - 96);
            pin = y + z;
            NameAccount = "" + char.ToLower(firstName[0]) + "" + char.ToLower(lastName[0])
                + "" + "-" + (firstName.Length + lastName.Length) + "-" + y + "-" + z;
        }

        public AccountBank createAccountsBank(AccountType accountType, int userId)
        {
            return new AccountBank()
            {
                Name = NameAccount,
                accountType = accountType,
                UserId = userId,
            };
        }
    }
}
