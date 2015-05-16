using System;

namespace GM.Core.Models
{
    public abstract class PieceComptable
    {
        public long Id { get; set; }
        public int FournisseurId { get; set; }
        public int ClientId { get; set; }
        public string NumPiece { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Tva { get; set; }
        public decimal? Ttc { get; set; }
        public decimal? Tht { get; set; }
        
        
    }
}
