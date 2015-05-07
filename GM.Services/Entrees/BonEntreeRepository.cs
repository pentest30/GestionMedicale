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
    public class BonEntreeRepository:IRepository<BonEntree>
    {
       private readonly PharmacieContext _db;

       public BonEntreeRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<BonEntree> SelectAll()
        {
            return _db.BonEntrees.Include("LigneEntreesMagasin");
        }

        public BonEntree SelectById(object id)
        {
            return _db.BonEntrees.Find(id);
        }

        public void Insert(BonEntree item)
        {
            _db.BonEntrees.Add(item);
            Save();
        }

        public void Update(BonEntree item)
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

        public IEnumerable<BonEntree> Find(Func<BonEntree, bool> predicate)
        {
            return _db.BonEntrees.Include(x => x.LigneEntreesMagasin).Include("Fournisseur").Where(predicate);
        }

        public BonEntree FindSingle(Func<BonEntree, bool> predicate)
        {
            return _db.BonEntrees.Include(x=>x.LigneEntreesMagasin).Include("Fournisseur").FirstOrDefault(predicate);
        }

        public IEnumerable<BonEntree> GetAllLazyLoad(params Expression<Func<BonEntree, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<BonEntree, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
