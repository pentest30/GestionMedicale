using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Medicaments
{
    public class RembouressementRepository:IRepository<Remboursement>
    {
        private readonly PharmacieContext _db;

        public RembouressementRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Remboursement> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Remboursement SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Remboursement item)
        {
            _db.Remboursements.Add(item);
            Save();
        }

        public void Update(Remboursement item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Remboursements.Find(id);
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Remboursement> Find(Func<Remboursement, bool> predicate)
        {
            return _db.Remboursements.Where(predicate);
        }

        public Remboursement FindSingle(Func<Remboursement, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Remboursement> GetAllLazyLoad(params Expression<Func<Remboursement, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Remboursement, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
