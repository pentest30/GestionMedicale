using System.Data.Entity;
using GM.Core.Models;

namespace GM.Context
{
    public class PharmacieContext : DbContext
    {
        public PharmacieContext()
            : base("name=DefaultConnection")
        {
            
        }

        public DbSet<Pays> Payses { get; set; }
       
        public DbSet<Pharmacie> Pharmacies { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<Dci> Dcis { get; set; }
        public DbSet<Conditionnement> Conditionnements { get; set; }
        public DbSet<Forme> Formes { get; set; }
        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Remboursement> Remboursements { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<ParamStock> ParamStocks { get; set; }
        public DbSet<BonEntree> BonEntrees { get; set; }
        public DbSet<LigneEntreeMagasin> LigneEntreeMagasins { get; set; }
        public DbSet<MedicamentPicture> MedicamentPicture { get; set; }
        public DbSet<PointVente> PointVentes { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<LigneCommande> LigneCommandes { get; set; }
        public DbSet<LigneEntree> LigneEntrees { get; set; }
        public DbSet<Entree> Entrees { get; set; }
       // public DbSet<LigneEntreeMagasin> LigneEntreeMagasins { get; set; }



    }
}
