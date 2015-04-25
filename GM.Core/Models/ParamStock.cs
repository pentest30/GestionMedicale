using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Core.Models
{
    public class ParamStock
    {
        [Column("Id")]
        [Key]
        public int ParmsId { get; set; }
       // [ForeignKey("Medicament")]
        public int MedicamentId { get; set; }
        public int EntrepriseId { get; set; }
        public int? PointVenteId { get; set; }
        //public Medicament Medicament { get; set; }
        public Entreprise Entreprise { get; set; }
        public PointVente PointVente { get; set; }
        [Display(Name = "Qnantité Minimale")]
        public int QntMinimale { get; set; }
        [Display(Name = "Qnantité criqtique")]
        public int QntCritique { get; set; }


    }
}
