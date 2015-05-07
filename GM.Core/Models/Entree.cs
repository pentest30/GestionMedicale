using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Entree
    {
        public Entree()
        {
            LigneEntrees = new List<LigneEntree>();
        }
        public long Id { get; set; }
        [Display(Name = "Fournisseur")]
        [Required]
        public int FournisseurId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Display(Name = "N de facture")]
        [Required]
        public string NumPiece { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public decimal? Tva { get; set; }

        public decimal? Ttc { get; set; }

        public decimal? Tht { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public IList<LigneEntree> LigneEntrees { get; set; }


    }
}
