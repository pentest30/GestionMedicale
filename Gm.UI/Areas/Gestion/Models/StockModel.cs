namespace Gm.UI.Areas.Gestion.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public int MedicamentId { get; set; }
        public int MagasinId { get; set; }
        public long? Qnt { get; set; }
        public int EntrepriseId { get; set; }
        public string NomCommerciale { get; set; }
        public string Dose { get; set; }
        public string Forme { get; set; }
        public int QntCritique { get; set; }
        public string Magasin { get; set; }


    }
}