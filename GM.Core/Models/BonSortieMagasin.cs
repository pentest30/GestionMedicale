using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class BonSortieMagasin
    {
        public long Id { get; set; }

        [Display(Name = "Fournisseur")]
        [Required]
        public int FournisseurId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Display(Name = "N Bon d'entrée")]
        [Required]
        public string NumPiece { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Magasin")]
        [Required]
        public int MagasinId { get; set; }

        public Magasin Magasin { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }
}
