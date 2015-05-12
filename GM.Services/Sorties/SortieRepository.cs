using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;
using EntityState = System.Data.Entity.EntityState;

namespace GM.Services.Sorties
{
    public class SortieRepository:IRepository<BonSortieMagasin>
    {
         private readonly PharmacieContext _db;

         public SortieRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<BonSortieMagasin> SelectAll()
        {
            return _db.BonSortieMagasins.Include("LigneSortieMagasin");
        }

        public BonSortieMagasin SelectById(object id)
        {
            return _db.BonSortieMagasins.Find(id);
        }

        public void Insert(BonSortieMagasin item)
        {
            _db.BonSortieMagasins.Add(item);
            Save();
        }

        public void Update(BonSortieMagasin item)
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

        public IEnumerable<BonSortieMagasin> Find(Func<BonSortieMagasin, bool> predicate)
        {
            return _db.BonSortieMagasins.Include("Fournisseur").Where(predicate);
        }

        public BonSortieMagasin FindSingle(Func<BonSortieMagasin, bool> predicate)
        {
            return _db.BonSortieMagasins.Include("Fournisseur").FirstOrDefault(predicate);
        }

        public IEnumerable<BonSortieMagasin> GetAllLazyLoad(params Expression<Func<BonSortieMagasin, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<BonSortieMagasin, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
