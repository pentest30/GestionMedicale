using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Dci
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Code { get; set; }

    }
}
