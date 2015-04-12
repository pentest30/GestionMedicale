using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Core.Models
{
    public class ParamStock
    {
        public int Id { get; set; }
        [ForeignKey("Medicament")]
        public int MedicamentId { get; set; }
        public int EntrepriseId { get; set; }
        public Medicament Medicament { get; set; }
        public Entreprise Entreprise { get; set; }
        public int QntMinimale { get; set; }
        public int QntCritique { get; set; }


    }
}
