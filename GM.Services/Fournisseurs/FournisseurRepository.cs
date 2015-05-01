using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Fournisseurs
{
    public class FournisseurRepository:IRepository<Fournisseur>
    {
         private readonly PharmacieContext _db;

         public FournisseurRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Fournisseur> SelectAll()
        {
            return _db.Fournisseurs;
        }

        public Fournisseur SelectById(object id)
        {
            var item=_db.Fournisseurs.Find(id);
            return item;
        }

        public void Insert(Fournisseur item)
        {
            _db.Fournisseurs.Add(item);
            Save();
        }

        public void Update(Fournisseur item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Fournisseurs.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Fournisseur> Find(Func<Fournisseur, bool> predicate)
        {
            return _db.Fournisseurs.Where(predicate);
        }

        public Fournisseur FindSingle(Func<Fournisseur, bool> predicate)
        {
            return _db.Fournisseurs.FirstOrDefault(predicate);
        }

        public IEnumerable<Fournisseur> GetAllLazyLoad(params Expression<Func<Fournisseur, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Fournisseur, bool> predicate)
        {
            return _db.Fournisseurs.Any(predicate);
        }
    }
}
