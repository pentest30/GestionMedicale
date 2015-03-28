using System;

namespace GM.Core.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Nrc { get; set; }
        public string Nif { get; set; }
        public string Nis { get; set; }
        public decimal? Capital { get; set; }
        public DateTime? DateFondation { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SiteWeb { get; set; }
        public Guid PropreitaireId { get; set; }
        public string CompteBancaire { get; set; }
        public Utilisateur Propreitaire{ get; set; }
        public string Logo { get; set; }


    }
}
