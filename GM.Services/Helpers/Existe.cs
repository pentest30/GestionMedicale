using System.ComponentModel.DataAnnotations;
using System.Linq;
using GM.Context;

namespace GM.Services.Helpers
{
    public class Existe : ValidationAttribute
    {
       
        public override bool IsValid(object value)
        {
            using (var db = new PharmacieContext())
            {
                if (value == null) return false;
                return !db.Medicaments.Any(x => x.NomCommerciale.Equals(value.ToString()));
            }
        }
    }
}
