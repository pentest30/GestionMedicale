namespace Gm.UI.Areas.Gestion.Models
{
    public class LigneComamndeModel
    {
        public long Id { get; set; }
        public long CommandeId { get; set; }
        public int MedicamentId { get; set; }
        public int Qnt { get; set; }
        //public Commande Commande { get; set; }

        public MedicamentModel Medicament { get; set; }
       
    }
}