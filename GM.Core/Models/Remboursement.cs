using System;

namespace GM.Core.Models
{
    public class Remboursement
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int MedicamentId { get; set; }
        public decimal? TarifReference { get; set; }
        public Medicament Medicament { get; set; }

    }
}
