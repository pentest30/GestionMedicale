using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Magasins
{
    public class MagasinRepository:IRepository<Magasin>
    {
         private readonly PharmacieContext _db;

        public MagasinRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Magasin> SelectAll()
        {
            return _db.Magasins;
        }

        public IEnumerable<Pays> ListePays()
        {
            return _db.Payses;
        } 

        public Magasin SelectById(object id)
        {
            var item = _db.Magasins.Find(id);
            return item;
        }

        public void Insert(Magasin item)
        {
            _db.Magasins.Add(item);
            Save();
        }

        public void Update(Magasin item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Magasins.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Magasin> Find(Func<Magasin, bool> predicate)
        {
            return _db.Magasins.Where(predicate);
        }

        public Magasin FindSingle(Func<Magasin, bool> predicate)
        {
            return _db.Magasins.FirstOrDefault(predicate);
        }

        public IEnumerable<Magasin> GetAllLazyLoad(params Expression<Func<Magasin, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Magasin, bool> predicate)
        {
            return _db.Magasins.Any(predicate);
        }
    }
}
