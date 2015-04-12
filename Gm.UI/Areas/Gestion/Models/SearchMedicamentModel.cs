using System.ComponentModel.DataAnnotations;

namespace Gm.UI.Areas.Gestion.Models
{
    public class SearchMedicamentModel
    {
         //public int Id { get; set; }
        [Required]
        [Display(Name = "Nom commerciale")]
        public string NomCommerciale { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        //Required]
        [Display(Name = "N d'enregistrement")]
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
        [Display(Name = "Fabriquant")]
        [Required]
        public int LaboratoireId { get; set; }
        
    }
}