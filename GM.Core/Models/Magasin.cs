namespace GM.Core.Models
{
    public class Magasin
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int EntrepriseId { get; set; }
        public int ? PointVenteId { get; set; }
        public Entreprise Entreprise { get; set; }
        public PointVente PointVente { get; set; }
    }
}
