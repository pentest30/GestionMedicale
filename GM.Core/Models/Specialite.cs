using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Specialite
    {
       // [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public string Code { get; set; }

    }
}
