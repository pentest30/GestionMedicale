using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Conditionnelts
{
    public class ConditionnementRepository:IRepository<Conditionnement>
    {
        private readonly PharmacieContext _db;

        public ConditionnementRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Conditionnement> SelectAll()
        {
            return _db.Conditionnements;
        }

        public Conditionnement SelectById(object id)
        {
            return (id == null) ? new Conditionnement() : _db.Conditionnements.Find(id);
        }

        public void Insert(Conditionnement item)
        {
            _db.Conditionnements.Add(item);
            Save();
        }

        public void Update(Conditionnement item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = SelectById(id);
            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
                Save();
            }

        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Conditionnement> Find(Func<Conditionnement, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Conditionnement FindSingle(Func<Conditionnement, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Conditionnement> GetAllLazyLoad(params Expression<Func<Conditionnement, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Conditionnement, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
