using System;
using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Models
{
    public class RegisterModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime? DateNaissance { get; set; }

        [Required]
        public string Addresse { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "N de Tél")]
        public string Tel { get; set; }

        [Display(Name = "N de FAX")]
        public string Fax { get; set; }

        [Display(Name = "Mot de pass")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirmer le mot de pass")]
        public string ConfirmerPassword { get; set; }
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string Pseudo { get; set; }
        // public DateTime? DateInscription { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        [Display(Name = "Photo personnel")]
        public string Photo { get; set; }
        [Display(Name = "Piece d'identité")]
        public string Piece { get; set; }   
        //public int? LocalId { get; set; }
    }
}