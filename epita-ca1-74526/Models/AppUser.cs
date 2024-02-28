using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace epita_ca1_74526.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
      
        public string? firstName { get; set; }

        public string? lastName { get; set; }    
        public string? accountNumber { get; set; }

        public string? pin { get; set; }
        // Constructeur pour initialiser le code PIN automatiquement
       /* public AppUser()
        {
            SetPin(); // Appeler la méthode setPin lors de la création d'une instance d'AppUser
        }

        // Méthode pour définir le code PIN
        private void SetPin()
        {
            pin = firstName.Substring(0, 2).ToLower() + lastName.Substring(0, 2).ToLower(); // Exemple de génération de code PIN basé sur les deux premières lettres du prénom et du nom de famille
        }*/
    }
}
