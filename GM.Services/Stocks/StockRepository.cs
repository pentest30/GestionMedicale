using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Stocks
{
    public class StockRepository:IRepository<Stock>
    {
        public IEnumerable<Stock> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Stock SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Stock item)
        {
            throw new NotImplementedException();
        }

        public void Update(Stock item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> Find(Func<Stock, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Stock FindSingle(Func<Stock, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetAllLazyLoad(params Expression<Func<Stock, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Stock, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
