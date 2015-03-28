using System;


namespace GM.Core.Models
{
    public class UtilisateurRole
    {
        public Guid UtilisateurId { get; set; }
        public int RoleId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public virtual Role Roles { get; set; }
        public int Id { get; set; }
    }
}
