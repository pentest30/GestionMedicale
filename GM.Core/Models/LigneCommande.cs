namespace GM.Core.Models
{
    public class LigneCommande
    {
        public long Id { get; set; }
        public long CommandeId { get; set; }
        public int MedicamentId { get; set; }
        public int  Qnt { get; set; }
        public Commande Commande { get; set; }
        public Medicament Medicament { get; set; }
    }
}
