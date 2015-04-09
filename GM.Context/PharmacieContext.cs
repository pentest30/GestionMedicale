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
        public DbSet<LigneEntree> LigneEntrees { get; set; }



    }
}
