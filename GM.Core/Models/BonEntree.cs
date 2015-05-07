using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class BonEntree
    {
        public BonEntree()
        {
            LigneEntreesMagasin = new List<LigneEntreeMagasin>();
        }

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

        public decimal? Tva { get; set; }

        public decimal? Ttc { get; set; }

        public decimal? Tht { get; set; }

        [Display(Name = "Magasin")]
        [Required]
        public int MagasinId { get; set; }

        public Magasin Magasin { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public long FactureId { get; set; }
        public Entree Facture { get; set; }


        public IList<LigneEntreeMagasin> LigneEntreesMagasin { get; set; }
    }
}
