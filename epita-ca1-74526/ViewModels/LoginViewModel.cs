using System.ComponentModel.DataAnnotations;
// Name: Lucile Pelou
// Student number: 74526
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

        public string SelectedRole { get; set; }

    }
}
