using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Stocks
{
    public class ServiceStock:IServiceStock
    {
        private readonly IRepository<Stock> _repository;

        public ServiceStock(IRepository<Stock > repository )
        {
            _repository = repository;
        }

        public IEnumerable<Stock> Liste(int id)
        {
            return _repository.Find(x => x.EntrepriseId == id);
        }

        public bool Insert(Stock stock)
        {
            try
            {
                _repository.Insert(stock);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insert(Stock stock, out int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Stock stock)
        {

            try
            {
                _repository.Update(stock);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Stock FindSingle(int id , int entrepriseId)
        {
            return _repository.FindSingle(x => x.MedicamentId == id &&x.MagasinId == entrepriseId);
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
