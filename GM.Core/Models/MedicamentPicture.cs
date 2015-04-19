using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class MedicamentPicture
    {
        public int Id { get; set; }
        [DataType(DataType.ImageUrl)]
        [Required]
        public string ImageUrl { get; set; }
        public int MedicamentId{ get; set; }
        //public Medicament Medicament { get; set; }
    }
}
