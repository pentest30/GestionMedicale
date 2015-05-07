using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class LigneEntree
    {

        public int Id { get; set; }

        [Display(Name = "Médicament")]
        public int MedicamentId { get; set; }

        [Display(Name = "N Lot")]
        public string NumLot { get; set; }

        [Display(Name = "Qnantité")]
        public int Qnt { get; set; }

        public decimal Montant { get; set; }

        [Display(Name = "Prix vente")]
        public decimal PrixVente { get; set; }

        [Display(Name = "Prix achat")]
        public decimal PrixAchat { get; set; }

        public long EntreeId { get; set; }

        [Display(Name = "Date de fabrication ")]
        [DataType(DataType.Date)]
        public DateTime? DateFabrication { get; set; }

        [Display(Name = "Date de péremption ")]
        [DataType(DataType.Date)]
        public DateTime? DatePeremption { get; set; }

        [Display(Name = "Taux de benifice")]

        public decimal? TauxBenifice { get; set; }
        
        //public Entree Entree { get; set; }
        public Medicament Medicament { get; set; }
    }
}
