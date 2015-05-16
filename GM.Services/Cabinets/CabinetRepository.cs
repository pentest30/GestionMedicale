using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Cabinets
{
    public class CabinetRepository:IRepository<Cabinet>
    {
        private readonly PharmacieContext _db;

        public CabinetRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Cabinet> SelectAll()
        {
            return _db.Cabinets;
        }

        public Cabinet SelectById(object id)
        {
            var item = _db.Cabinets.Find(id);
            return item;
        }

        public void Insert(Cabinet item)
        {
            _db.Cabinets.Add(item);
            Save();
        }

        public void Update(Cabinet item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Cabinets.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Cabinet> Find(Func<Cabinet, bool> predicate)
        {
            return _db.Cabinets.Where(predicate);
        }

        public Cabinet FindSingle(Func<Cabinet, bool> predicate)
        {
            return _db.Cabinets.FirstOrDefault(predicate);
        }

        public IEnumerable<Cabinet> GetAllLazyLoad(params Expression<Func<Cabinet, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Cabinet, bool> predicate)
        {
            return _db.Cabinets.Any(predicate);
        }
    }
}
