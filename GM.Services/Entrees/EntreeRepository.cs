using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Entrees
{
    public class EntreeRepository:IRepository<Entree>
    {
         private readonly PharmacieContext _db;

         public EntreeRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Entree> SelectAll()
        {
            return _db.Entrees.Include(c =>c.LigneEntrees);
        }

        public Entree SelectById(object id)
        {
            return _db.Entrees.Find(id);
        }

        public void Insert(Entree item)
        {
            _db.Entrees.Add(item);
            Save();
        }

        public void Update(Entree item)
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

        public IEnumerable<Entree> Find(Func<Entree, bool> predicate)
        {
            return _db.Entrees.Include(c => c.LigneEntrees).Include("Fournisseur").Where(predicate);
        }

        public Entree FindSingle(Func<Entree, bool> predicate)
        {
            return _db.Entrees.Include(c => c.LigneEntrees).FirstOrDefault(predicate);
        }

        public IEnumerable<Entree> GetAllLazyLoad(params Expression<Func<Entree, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Entree, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
