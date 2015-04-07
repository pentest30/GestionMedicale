using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Models.Utilisateurs
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Identifiant (ou Email)  *")]
        public string Identifiant { get; set; }
        [Required]
        [Display(Name = "Password *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         [Display(Name = "Souvien de moi")]
        public bool RememberMe { get; set; }
    }
}