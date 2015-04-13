using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Fabriquant
{
    public class ServiceFabriquant:IServiceFabriquant
    {
        private readonly IRepository<Laboratoire> _repository;

        public ServiceFabriquant(IRepository<Laboratoire>repository )
        {
            _repository = repository;
        }

        public IEnumerable<Laboratoire> Liste()
        {
            return _repository.SelectAll();
        }

        public bool Insert(Laboratoire laboratoire)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Laboratoire laboratoire)
        {
            throw new System.NotImplementedException();
        }

        public Laboratoire FindSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
