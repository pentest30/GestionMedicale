using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Cabinet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Champ nom est requis")]
        public string Nom { get; set; }

        [Required]

        public string Wilaya { get; set; }

        [Required]
        public string Commune { get; set; }

        public string Email { get; set; }

        //[DataType(DataType.MultilineText)]

        [Required]
        public string Addresse { get; set; }

        public string Tel { get; set; }
        public Guid MedecinId { get; set; }
        public Utilisateur Medecin { get; set; }

    }
}
