using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Fabriquant
{
    public class FabriquantRepository:IRepository<Laboratoire>
    {
        private readonly PharmacieContext _db;

        public FabriquantRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Laboratoire> SelectAll()
        {
            return _db.Laboratoires;
        }

        public IEnumerable<Pays> ListePays()
        {
            return _db.Payses;
        } 

        public Laboratoire SelectById(object id)
        {
            var item = _db.Laboratoires.Find(id);
            return item;
        }

        public void Insert(Laboratoire item)
        {
            _db.Laboratoires.Add(item);
            Save();
        }

        public void Update(Laboratoire item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Laboratoires.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Laboratoire> Find(Func<Laboratoire, bool> predicate)
        {
            return _db.Laboratoires.Where(predicate);
        }

        public Laboratoire FindSingle(Func<Laboratoire, bool> predicate)
        {
            return _db.Laboratoires.FirstOrDefault(predicate);
        }

        public IEnumerable<Laboratoire> GetAllLazyLoad(params Expression<Func<Laboratoire, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Laboratoire, bool> predicate)
        {
            return _db.Laboratoires.Any(predicate);
        }
    }
}
