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
    public class LigneEntreeRepo:IRepository<LigneEntree>
    {
         private readonly PharmacieContext _db;

         public LigneEntreeRepo(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<LigneEntree> SelectAll()
        {
            throw new NotImplementedException();
        }

        public LigneEntree SelectById(object id)
        {
            return _db.LigneEntrees.Find(id);
        }

        public void Insert(LigneEntree item)
        {
            _db.LigneEntrees.Add(item);
            Save();
        }

        public void Update(LigneEntree item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = SelectById(id);
            if (item == null) return;
            _db.Entry(item).State= EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<LigneEntree> Find(Func<LigneEntree, bool> predicate)
        {
           return _db.LigneEntrees.Include(x=>x.Medicament).Where(predicate);
        }

        public LigneEntree FindSingle(Func<LigneEntree, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LigneEntree> GetAllLazyLoad(params Expression<Func<LigneEntree, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<LigneEntree, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
