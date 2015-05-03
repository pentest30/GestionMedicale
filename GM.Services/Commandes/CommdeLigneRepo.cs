using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Commandes
{
    public class CommdeLigneRepo:IRepository<LigneCommande>
    {
        private readonly PharmacieContext _db;

        public CommdeLigneRepo(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<LigneCommande> SelectAll()
        {
            throw new NotImplementedException();
        }

        public LigneCommande SelectById(object id)
        {
            return _db.LigneCommandes.Find(id);
        }

        public void Insert(LigneCommande item)
        {
            _db.LigneCommandes.Add(item);
            Save();
        }

        public void Update(LigneCommande item)
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

        public IEnumerable<LigneCommande> Find(Func<LigneCommande, bool> predicate)
        {
           return _db.LigneCommandes.Include(x=>x.Medicament).Where(predicate);
        }

        public LigneCommande FindSingle(Func<LigneCommande, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LigneCommande> GetAllLazyLoad(params Expression<Func<LigneCommande, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<LigneCommande, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
