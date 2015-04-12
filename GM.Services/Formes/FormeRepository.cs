using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Formes
{
    public class FormeRepository : IRepository<Forme>
    {
        private readonly PharmacieContext _db;

        public FormeRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Forme> SelectAll()
        {
            return _db.Formes;
        }

        public Forme SelectById(object id)
        {
            return (id == null) ? new Forme() : _db.Formes.Find(id);
        }

        public void Insert(Forme item)
        {
            _db.Formes.Add(item);
            Save();
        }

        public void Update(Forme item)
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

        public IEnumerable<Forme> Find(Func<Forme, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Forme FindSingle(Func<Forme, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forme> GetAllLazyLoad(params Expression<Func<Forme, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Forme, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
