using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using GM.Core.Models;

namespace Gm.UI.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            UtilisateurRoles  = new Collection<UtilisateurRole>();
        }
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nom *")]
        
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom *")]
        public string Prenom { get; set; }

        //[Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime? DateNaissance { get; set; }

       // [Required]
        [DataType(DataType.MultilineText)]
        public string Addresse { get; set; }

        [Required]
        [Display(Name = "Email *")]
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Tél")]
        public string Tel { get; set; }
     
        [Display(Name = "Mot de pass *")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de pass *")]
        public string ConfirmerPassword { get; set; }
        [Required]
        [Display(Name = "Identifiant *")]
        public string Pseudo { get; set; }
        // public DateTime? DateInscription { get; set; }
        [Display(Name = "Type de compte *")]
        [Required]
        public int RoleId { get; set; }
        [Display(Name = "Photo personnel")]
        public string Photo { get; set; }
        [Display(Name = "Piece d'identité")]
        public string Piece { get; set; }
        [Required]
        [Display(Name = "Genre *")]
        public string Sexe { get; set; }
        [Required(ErrorMessage = "Valeur non Valide por le champ Jours")]
        [Display(Name = "Jours")]
        public int? DateOfBirthDay { get; set; }
        [Required(ErrorMessage = "Valeur non Valide por le champ Mois")]
        [Display(Name = "Mois")]
        public int? DateOfBirthMonth { get; set; }
        [Required(ErrorMessage = "Valeur non Valide por le champ Année")]
        [Display(Name = "Année")]
        public int? DateOfBirthYear { get; set; }
        [Required]
        [Display(Name = "Wilaya *")]
        [UIHint("Wilaya")]
        public string   Wilaya { get; set; }

        public string Filter { get; set; }
        public int? LocalId { get; set; }
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }
    }
}