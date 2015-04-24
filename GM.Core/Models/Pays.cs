using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Core.Models
{
    [Table("country")]
    public class Pays
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("name")]
        public string Nom { get; set; }
    }
}
