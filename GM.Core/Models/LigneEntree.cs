using System;

namespace GM.Core.Models
{
    public class LigneEntree : ILigneCommande
    {
        public LigneEntree()
        {
            BonEntree = new BonEntree();
        }
        public int Id { get; set; }

        public int MedicamentId { get; set; }

        public string NumLot { get; set; }

        public int Qnt { get; set; }

        //public decimal? Montant { get; set; }

        public decimal PrixUnitaire { get; set; }
        public long BonEntreeId { get; set; }
        public DateTime? DateFabrication { get; set; }
        public DateTime? DatePeremption { get; set; }
        public decimal? TauxBenifice { get; set; }
        public BonEntree BonEntree { get; set; }
        public Medicament Medicament { get; set; }
    }
}
