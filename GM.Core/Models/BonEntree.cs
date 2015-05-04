using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class BonEntree 
    {
        public BonEntree()
        {
            LigneEntrees = new List<LigneEntree>();
        }

        public long Id { get; set; }
         [Display(Name = "Fournisseur")]
         [Required]
        public int FournisseurId { get; set; }
         [Required]
        public int ClientId { get; set; }
        [Display(Name = "N Bon d'netrée")]
        [Required]
        public string NumPiece { get; set; }
         [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public decimal? Tva { get; set; }

        public decimal? Ttc { get; set; }

        public decimal? Tht { get; set; }
        [Display(Name = "Magasin")]
        [Required]
        public int MagasinId { get; set; }
        public Magasin Magasin { get; set; }
        public Fournisseur Fournisseur { get; set; }


        public IEnumerable<LigneEntree> LigneEntrees { get; set; }
    }
}
