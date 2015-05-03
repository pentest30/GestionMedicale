using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Core.Models
{
    public class LigneCommande
    {
       
        public long Id { get; set; }
        [Required]
        public long CommandeId { get; set; }
        [Display(Name =  "Medicament")]
        [Required]
        public int MedicamentId { get; set; }
        public int  Qnt { get; set; }
        //public Commande Commande { get; set; }
        
        public   Medicament Medicament { get; set; }
        [NotMapped]
        public string Dose { get; set; }
        [NotMapped]
        public string Forme { get; set; }

    }
}
