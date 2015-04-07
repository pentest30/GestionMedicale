using System;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Pharmacies
{
    public class ServicePharmacie:IServicePharmacie
    {
        private readonly IRepository<Pharmacie> _repository;

        public ServicePharmacie(IRepository<Pharmacie>repository )
        {
            _repository = repository;
        }

        public int? GetPharmacie(Guid id)
        {
            return _repository.FindSingle(x => x.PropreitaireId == id).Id;
        }
    }
}
