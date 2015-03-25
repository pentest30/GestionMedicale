using System.Data.Entity;
using GM.Core.Models;

namespace GM.Context
{
    public class UserContext : DbContext

    {
        public UserContext()
            : base("DefaultConnection")
        {
            
        }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UtilisateurRole> UtilisateurRoles { get; set; }
        public DbSet<Banne> Bannes { get; set; }
       


    }
}
