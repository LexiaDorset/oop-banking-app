using System.ComponentModel.DataAnnotations;

namespace epita_ca1_74526.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Name of account")]
        [Required(ErrorMessage = "Name of account is required")]

        public string? NameAccount { get; set; }

    }
}
