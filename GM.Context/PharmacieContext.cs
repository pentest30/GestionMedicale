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
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
    }
}
