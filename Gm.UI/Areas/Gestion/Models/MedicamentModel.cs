using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Gm.UI.HtmlExtenssions;
using GM.Services.Helpers;

namespace Gm.UI.Areas.Gestion.Models
{
    public class MedicamentModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom commerciale")]
        [Remote("ExisteResult", "Medicament", HttpMethod = "POST", ErrorMessage = "Ce nom déja exister choiser un autre nom commerciale.")]
        public string NomCommerciale { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        //Required]
        [Display(Name = "N° d'enregistrement")]
        public string NumEnregistrement { get; set; }
        [Display(Name = "Dose")]
        public string Dose { get; set; }
        public bool Remboursable { get; set; }
        [Required]
        [Display(Name = "DCI")]
        public int DciId { get; set; }
        [Display(Name = "Spécialité")]
        [Required]
        public int SpecialiteId { get; set; }
        [Display(Name = "Forme")]
        [Required]
        public int FormeId { get; set; }
        [Display(Name = "Conditionnement")]
        [Required]
        public int ConditionnementId { get; set; }
        [Display(Name = "Fabriquant")]
        [Required]
        public int? LaboratoireId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Discription { get; set; }
        //[UIHint("Number")]
        public decimal? Tva { get; set; }
        public string Dci { get; set; }
        public string Specialite { get; set; }
        public string Forme { get; set; }
        public string Conditionnement { get; set; }

    }
}