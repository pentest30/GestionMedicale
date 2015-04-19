using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Conditionnelts
{
    public class ServiceConditionnement:IServiceConditionnement
    {
        private readonly IRepository<Conditionnement> _repository;

        public ServiceConditionnement(IRepository<Conditionnement> repository )
        {
            _repository = repository;
        }

        public IEnumerable<Conditionnement> Liste()
        {
            return _repository.SelectAll();
        }

        public bool Insert(Conditionnement conditionnement)
        {
            try
            {
                _repository.Insert(conditionnement);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Conditionnement conditionnement)
        {
            try
            {
                _repository.Update(conditionnement);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public Conditionnement FindSingle(int id)
        {
            throw new System.NotImplementedException();
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
