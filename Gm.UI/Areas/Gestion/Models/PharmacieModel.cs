using System;
using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Areas.Gestion.Models
{
    public class PharmacieModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom *")]
        public string Nom { get; set; }

        [Display(Name = "Numéro de registe de commerce")]
        public string Nrc { get; set; }

        [Display(Name = "Numéro d'identificateur fiscal")]
        public string Nif { get; set; }

        [Display(Name = "Numéro d'identificateur Statique")]
        public string Nis { get; set; }

        [Display(Name = "Chiffre d'affaire")]
        [DataType(DataType.Currency)]
        public decimal? Capital { get; set; }

        [Display(Name = "Date de fondation")]
        [DataType(DataType.Date)]
        public DateTime? DateFondation { get; set; }

        [Required]
        [Display(Name = "Tél *")]
        public string Tel { get; set; }

        public string Fax { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Forme d'addresse email non valide")]
        public string Email { get; set; }

        [Display(Name = "Site web")]
        public string SiteWeb { get; set; }

        [Required]
        public Guid? PropreitaireId { get; set; }

        [Required]
        [Display(Name = "Wilaya *")]
        public string Wilaya { get; set; }

        [Required]
        [Display(Name = "Commune *")]
        public string Commune { get; set; }

        [Display(Name = "Compte bancaire")]
        public string CompteBancaire { get; set; }

        public string Logo { get; set; }
    }
}