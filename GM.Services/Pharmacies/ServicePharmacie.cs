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
            var item = _repository.FindSingle(x => x.PropreitaireId == id);
            return (item!= null)? _repository.FindSingle(x => x.PropreitaireId == id).Id:0;
        }

        public bool Insert(Pharmacie pharmacie)
        {
            try
            {
                _repository.Insert(pharmacie);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Pharmacie pharmacie)
        {
            try
            {
                _repository.Update(pharmacie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
