using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Formes
{
    public class ServiceForme:IServiceForme
    {
        private readonly IRepository<Forme> _repository;

        public ServiceForme(IRepository<Forme> repository )
        {
            _repository = repository;
        }

        public IEnumerable<Forme> ListeFormes()
        {
            return _repository.SelectAll();
        }

        public bool Insert(Forme forme)
        {
            try
            {
                _repository.Insert(forme );
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Forme forme)
        {
            try
            {
                _repository.Update(forme);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Forme FindSingle(int id)
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
