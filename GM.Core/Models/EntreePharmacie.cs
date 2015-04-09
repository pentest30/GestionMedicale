namespace GM.Core.Models
{
    public class EntreePharmacie
    {
        public int FournisseurId { get; set; }
        public int PharmacieId { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public Pharmacie Pharmacie { get; set; }
    }
}
