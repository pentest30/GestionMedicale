using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Pharmacies
{
    public class PharmacieRepository:IRepository<Pharmacie>
    {
        private readonly PharmacieContext _db;

        public PharmacieRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Pharmacie> SelectAll()
        {
            return _db.Pharmacies;
        }

        public Pharmacie SelectById(object id)
        {
            var item=_db.Pharmacies.Find(id);
            return item;
        }

        public void Insert(Pharmacie item)
        {
            _db.Pharmacies.Add(item);
            Save();
        }

        public void Update(Pharmacie item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Pharmacies.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Pharmacie> Find(Func<Pharmacie, bool> predicate)
        {
            return _db.Pharmacies.Where(predicate);
        }

        public Pharmacie FindSingle(Func<Pharmacie, bool> predicate)
        {
            return _db.Pharmacies.FirstOrDefault(predicate);
        }

        public IEnumerable<Pharmacie> GetAllLazyLoad(params Expression<Func<Pharmacie, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Pharmacie, bool> predicate)
        {
            return _db.Pharmacies.Any(predicate);
        }
    }
}
