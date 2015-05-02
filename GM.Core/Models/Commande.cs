using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Commande 
    {
        public Commande()
        {
            LigneCommandes = new List<LigneCommande>();
        }
        public long Id { get; set; }
        [Display(Name = "Fournisseur")]
        public int FournisseurId { get; set; }

        public int ClientId { get; set; }
        [Required]
        [Display(Name = "N commande")]
        public string NumPiece { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Tva { get; set; }

        public decimal? Ttc { get; set; }

        public decimal? Tht { get; set; }
        public bool Validation { get; set; }
        public bool Delivrance { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public IList<LigneCommande> LigneCommandes { get; set; }
       
    }
}
