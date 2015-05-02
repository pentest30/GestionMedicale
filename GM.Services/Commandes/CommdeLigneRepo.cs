using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void Update(LigneCommande item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LigneCommande> Find(Func<LigneCommande, bool> predicate)
        {
            throw new NotImplementedException();
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
