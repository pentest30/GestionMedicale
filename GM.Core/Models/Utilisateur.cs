using System;
using System.Collections.Generic;

namespace GM.Core.Models
{
    public class Utilisateur
    {
        public Utilisateur()
        {
            UtilisateurRoles = new List<UtilisateurRole>();
        }
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Addresse { get; set; } 
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string PasswordHash { get; set; }
        public string Pseudo   { get; set; }
        public string LienPhotoPersonnelle  { get; set; }
        public string LienPhotoDocument { get; set; }
        public bool Validation { get; set; }
        public DateTime? DateInscription { get; set; }

        public int? LocalId { get; set; }

        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }



    }
}
