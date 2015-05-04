using System; 

namespace GM.Core.Models
{
    public class LigneEntree
    {
        public LigneEntree()
        {
            Entree = new Entree();
        }
        public int Id { get; set; }

        public int MedicamentId { get; set; }

        public string NumLot { get; set; }

        public int Qnt { get; set; }
        public decimal Montant { get; set; }
        public decimal  PrixVente { get; set; }
        public decimal PrixAchat { get; set; }
        public long EntreeId { get; set; }
        public DateTime? DateFabrication { get; set; }
        public DateTime? DatePeremption { get; set; }
        public decimal? TauxBenifice { get; set; }
        public Entree Entree { get; set; }
        public Medicament Medicament { get; set; }
    }
}
