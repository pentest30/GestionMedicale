using System;
using System.ComponentModel.DataAnnotations;
using GM.Core.Models;

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
        [DataType(DataType.MultilineText)]
        public string Addresse { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Tél")]
        public string Tel { get; set; }

       
        [Display(Name = "Mot de pass")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirmer le mot de pass")]
        public string ConfirmerPassword { get; set; }
        [Required]
        [Display(Name = "Identifiant")]
        public string Pseudo { get; set; }
        // public DateTime? DateInscription { get; set; }
        [Display(Name = "Type de compte")]
        [Required]
        public int RoleId { get; set; }
        [Display(Name = "Photo personnel")]
        public string Photo { get; set; }
        [Display(Name = "Piece d'identité")]
        public string Piece { get; set; }
        [Required]
        public string Sexe { get; set; }
        [Required]
        [Display(Name = "Jour")]
        public int? DateOfBirthDay { get; set; }
        [Required]
        [Display(Name = "Mois")]
        public int? DateOfBirthMonth { get; set; }
        [Required]
        [Display(Name = "Année")]
        public int? DateOfBirthYear { get; set; }
      
        public int? LocalId { get; set; }
    }
}