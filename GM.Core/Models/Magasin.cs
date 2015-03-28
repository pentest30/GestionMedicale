namespace GM.Core.Models
{
    public class Magasin
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
