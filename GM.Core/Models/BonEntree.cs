using System;
using System.Collections.Generic;

namespace GM.Core.Models
{
    public class BonEntree : ICommandes
    {
        public BonEntree()
        {
            LignePieces = new List<LigneEntree>();
        }

        public long Id { get; set; }

        public int FournisseurId { get; set; }

        public int ClientId { get; set; }

        public string NumPiece { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Tva { get; set; }

        public decimal? Ttc { get; set; }

        public decimal? Tht { get; set; }

        public int MagasinId { get; set; }
        public Magasin Magasin { get; set; }

        public IEnumerable<ILigneCommande> LignePieces { get; set; }
    }
}
