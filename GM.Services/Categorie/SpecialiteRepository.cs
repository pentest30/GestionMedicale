using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Categorie
{
    public class SpecialiteRepository:IRepository<Specialite>
    {
        private readonly PharmacieContext _db;

        public SpecialiteRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Specialite> SelectAll()
        {
            return _db.Specialites;
        }

        public Specialite SelectById(object id)
        {
            var item=_db.Specialites.Find(id);
            return item;
        }

        public void Insert(Specialite item)
        {
            _db.Specialites.Add(item);
            Save();
        }

        public void Update(Specialite item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Specialites.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Specialite> Find(Func<Specialite, bool> predicate)
        {
            return _db.Specialites.Where(predicate);
        }

        public Specialite FindSingle(Func<Specialite, bool> predicate)
        {
            return _db.Specialites.FirstOrDefault(predicate);
        }

        public IEnumerable<Specialite> GetAllLazyLoad(params Expression<Func<Specialite, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Specialite, bool> predicate)
        {
            return _db.Specialites.Any(predicate);
        }
    }
}
