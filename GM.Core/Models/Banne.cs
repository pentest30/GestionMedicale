using System;

namespace GM.Core.Models
{
    public class Banne
    {
        public Guid UtilisateurId { get; set; }
        public int Id { get; set; }
        public DateTime? Debut { get; set; }
        public DateTime? Fin { get; set; }
        public Utilisateur Utilisateur { get; set; }


    }
}
