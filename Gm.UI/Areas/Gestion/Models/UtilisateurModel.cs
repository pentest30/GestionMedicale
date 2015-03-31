using System;
using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Areas.Gestion.Models
{
    public class UtilisateurModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Prénom: ")]
        public string Prenom { get; set; }
        [Display(Name = "Identifiant: ")]
        public string Pseudo { get; set; }

        public string RoleId { get; set; }
    }
}