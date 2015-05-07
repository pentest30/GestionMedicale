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
    public class LigneBonEntreeRepo:IRepository<LigneEntreeMagasin>
    {
         private readonly PharmacieContext _db;

         public LigneBonEntreeRepo(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<LigneEntreeMagasin> SelectAll()
        {
            throw new NotImplementedException();
        }

        public LigneEntreeMagasin SelectById(object id)
        {
            return _db.LigneEntreeMagasins.Find(id);
        }

        public void Insert(LigneEntreeMagasin item)
        {
            _db.LigneEntreeMagasins.Add(item);
            Save();
        }

        public void Update(LigneEntreeMagasin item)
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

        public IEnumerable<LigneEntreeMagasin> Find(Func<LigneEntreeMagasin, bool> predicate)
        {
           return _db.LigneEntreeMagasins.Include(x=>x.Medicament).Where(predicate);
        }

        public LigneEntreeMagasin FindSingle(Func<LigneEntreeMagasin, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LigneEntreeMagasin> GetAllLazyLoad(params Expression<Func<LigneEntreeMagasin, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<LigneEntreeMagasin, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
