using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Medicaments
{
    public class StockPramsRepository:IRepository<ParamStock>
    {
        private readonly PharmacieContext _db;

        public StockPramsRepository(PharmacieContext db)
        {
            _db = db;
        }

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
            _db.ParamStocks.Add(item);
            Save();
        }

        public void Update(ParamStock item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.ParamStocks.Find(id);
            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
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
