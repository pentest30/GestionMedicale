namespace GM.Core.Models
{
    public class LignePiece
    {
        public int Id { get; set; }
        public int MedicamentId { get; set; }
        public string NumLot { get; set; }
        public int Qnt { get; set; }
        public decimal? Montant { get; set; }
        public decimal PrixUnitaire { get; set; }


    }
}
