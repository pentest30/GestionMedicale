using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Medicaments
{
    public class StockPramsRepository:IRepository<ParamStock>
    {
        public IEnumerable<ParamStock> SelectAll()
        {
            throw new NotImplementedException();
        }

        public ParamStock SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ParamStock item)
        {
            throw new NotImplementedException();
        }

        public void Update(ParamStock item)
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

        public IEnumerable<ParamStock> Find(Func<ParamStock, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ParamStock FindSingle(Func<ParamStock, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParamStock> GetAllLazyLoad(params Expression<Func<ParamStock, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<ParamStock, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
