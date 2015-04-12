using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Models.Utilisateurs
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Identifiant *")]
        public string Identifiant { get; set; }

        [Required]
        [Display(Name = "Password *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi")]
        public bool RememberMe { get; set; }
    }
}