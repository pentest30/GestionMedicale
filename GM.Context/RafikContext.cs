using System.Data.Entity;
using GM.Core.Models;

namespace GM.Context
{
    public class RafikContext:DbContext
    {
        public RafikContext()
            : base("name=DefaultConnection")
        {
            
        }
        // classes ajouter ici
        //public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
