using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Nomenclature
{
    public class DciService:IServiceDci
    {
        private readonly IRepository<Dci> _repository;

        public DciService(IRepository<Dci> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Dci> ListeDcis()
        {
            return _repository.SelectAll();

        }

        public bool Insert(Dci dci)
        {

            try
            {
                _repository.Insert(dci);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
       
        }

        public bool Update(Dci dci)
        {
            try
            {
                _repository.Update(dci);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Dci FindSingle(int id)
        {
            return _repository.SelectById(id);
        }

        public bool Delete(int id)
        {
            //var dci = _repository.SelectById(id);
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

        public bool Existe(string code)
        {
            return _repository.Exist(x => x.Code .StartsWith( code.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
