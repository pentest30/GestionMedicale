using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Nomenclature
{
    public class DciRepository:IRepository<Dci>
    {
        private readonly PharmacieContext _db;

        public DciRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Dci> SelectAll()
        {
            return _db.Dcis.Include("Specialite");
        }

        public Dci SelectById(object id)
        {
            return (id == null) ? new Dci() : _db.Dcis.Find(id);
        }

        public void Insert(Dci item)
        {
            _db.Dcis.Add(item);
            Save();
        }

        public void Update(Dci item)
        {
            _db.Entry(item).State =EntityState.Modified;
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

        public IEnumerable<Dci> Find(Func<Dci, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Dci FindSingle(Func<Dci, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dci> GetAllLazyLoad(params Expression<Func<Dci, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Dci, bool> predicate)
        {
            return _db.Dcis.Any(predicate);
        }
    }
}
