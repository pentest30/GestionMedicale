using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Services.Fabriquant
{
    public class ServiceFabriquant:IServiceFabriquant
    {
        private readonly FabriquantRepository _repository;

        public ServiceFabriquant(FabriquantRepository repository )
        {
            _repository = repository;
        }

        public IEnumerable<Laboratoire> Liste()
        {
            return _repository.SelectAll();
        }

        public IEnumerable<Pays> ListePays()
        {
            return _repository.ListePays();
        }

        public bool Insert(Laboratoire laboratoire)
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

        public bool Update(Laboratoire laboratoire)
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

        public Laboratoire FindSingle(int id)
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
