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
    public class CommandeRepository:IRepository<Commande>
    {
        private readonly PharmacieContext _db;

        public CommandeRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Commande> SelectAll()
        {
            return _db.Commandes;
        }

        public Commande SelectById(object id)
        {
            return _db.Commandes.Find(id);
        }

        public void Insert(Commande item)
        {
            _db.Commandes.Add(item);
            Save();
        }

        public void Update(Commande item)
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

        public IEnumerable<Commande> Find(Func<Commande, bool> predicate)
        {
            return _db.Commandes.Where(predicate);
        }

        public Commande FindSingle(Func<Commande, bool> predicate)
        {
            return _db.Commandes.FirstOrDefault(predicate);
        }

        public IEnumerable<Commande> GetAllLazyLoad(params Expression<Func<Commande, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Commande, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
