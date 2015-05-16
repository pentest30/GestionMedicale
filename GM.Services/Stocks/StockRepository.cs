using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;
using GM.Services.Medicaments;
using EntityState = System.Data.Entity.EntityState;

namespace GM.Services.Stocks
{
    public class StockRepository:IRepository<Stock>
    {
         private readonly PharmacieContext _db;
        private readonly IServiceMedicmaent _serviceMedicmaent;

        public StockRepository(PharmacieContext db ,IServiceMedicmaent serviceMedicmaent)
        {
            _db = db;
            _serviceMedicmaent = serviceMedicmaent;
        }

        public IEnumerable<Stock> SelectAll()
        {
            return _db.Stocks.Include("Medicament").Include("Magasin");
        }

        public Stock SelectById(object id)
        {
            return _db.Stocks.Find(id);
        }

        public void Insert(Stock item)
        {
            _db.Stocks.Add(item);
            Save();
        }

        public void Update(Stock item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = SelectById(id);
            if (item == null) throw  new Exception();
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Stock> Find(Func<Stock, bool> predicate)
        {
            var result = _db.Stocks.Include("Magasin").Where(predicate);
            var enumerable = result as Stock[] ?? result.ToArray();
            foreach (var stock in enumerable)
            {
                stock.Medicament = _serviceMedicmaent.FindSingle(stock.MedicamentId);
            }
            return enumerable;
        }

        public Stock FindSingle(Func<Stock, bool> predicate)
        {
            return _db.Stocks.FirstOrDefault(predicate);
        }

        public IEnumerable<Stock> GetAllLazyLoad(params Expression<Func<Stock, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Stock, bool> predicate)
        {
            return _db.Stocks.Any(predicate);
        }
    }
}
