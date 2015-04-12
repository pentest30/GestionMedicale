using System;
using System.Collections.Generic;

namespace GM.Core.Models
{
    public interface ICommandes
    {
        long Id { get; set; }
        int FournisseurId { get; set; }
        int ClientId { get; set; }
        string NumPiece { get; set; }
        DateTime? Date { get; set; }
        decimal? Tva { get; set; }
        decimal? Ttc { get; set; }
        decimal? Tht { get; set; }
        IEnumerable<ILigneCommande> LignePieces { get; set; }
    }
}
