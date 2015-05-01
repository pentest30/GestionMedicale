using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Magasins
{
    public class ServiceMagasin:IServiceMagasin
    {
        private readonly IRepository<Magasin> _repository;

        public ServiceMagasin(IRepository<Magasin> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Magasin> Liste(int id)
        {
            return _repository.Find(x=>x.EntrepriseId == id);
        }

        public bool Insert(Magasin laboratoire)
        {
            try
            {
                _repository.Insert(laboratoire);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Magasin laboratoire)
        {
            try
            {
                _repository.Update(laboratoire);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Magasin FindSingle(int id)
        {
            return _repository.SelectById(id);
        }

        public bool Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
