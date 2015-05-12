using System.Collections.Generic;

namespace GM.Core.Models
{
    public class LigneSortieMagasin
    {
        public LigneSortieMagasin()
        {
            LigneEntreeMagasins = new List<LigneEntreeMagasin>();
        }
        public int Id { get; set; }

        public int MedicamentId { get; set; }

        public string NumLot { get; set; }

        public int Qnt { get; set; }
        //public decimal Montant { get; set; }
       
        public Medicament Medicament { get; set; }
        public IList<LigneEntreeMagasin> LigneEntreeMagasins { get; set; }
    }
}
