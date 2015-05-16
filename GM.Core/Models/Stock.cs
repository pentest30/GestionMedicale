namespace GM.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int MedicamentId { get; set; }
        public int MagasinId { get; set; }
        public long? Qnt { get; set; }
        public int EntrepriseId { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal TauxBenefice { get; set; }
        public Medicament Medicament { get; set; }
        public Magasin Magasin { get; set; }
        public Entreprise Entreprise { get; set; }

    }
}
