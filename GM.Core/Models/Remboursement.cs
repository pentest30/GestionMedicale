using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Core.Models
{
    public class Remboursement
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public int MedicamentId { get; set; }
        [Display(Name = "Tafir de réference")]
        public decimal? TarifReference { get; set; }
        //public Medicament Medicament { get; set; }

    }
}
