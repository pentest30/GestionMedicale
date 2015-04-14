using System.ComponentModel.DataAnnotations;
using GM.Services.Medicaments;

namespace GM.Services.Helpers
{
    public class Existe : ValidationAttribute
    {
        private readonly IServiceMedicmaent _service;

        public Existe(IServiceMedicmaent service)
        {
            _service = service;
        }

        public override bool IsValid(object value)
        {
            return _service.Existe(value.ToString());
        }
    }
}
