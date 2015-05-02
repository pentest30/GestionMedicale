using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class LigneCommande
    {
       
        public long Id { get; set; }
        public long CommandeId { get; set; }
        [Display(Name =  "Medicament")]
        public int MedicamentId { get; set; }
        public int  Qnt { get; set; }
        public Commande Commande { get; set; }
        public virtual Medicament Medicament { get; set; }
    }
}
