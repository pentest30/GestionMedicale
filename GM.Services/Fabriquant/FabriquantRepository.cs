using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Fabriquant
{
    public class FabriquantRepository:IRepository<Laboratoire>
    {
        private readonly PharmacieContext _db;

        public FabriquantRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Laboratoire> SelectAll()
        {
            return _db.Laboratoires;
        }

        public Laboratoire SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Laboratoire item)
        {
            throw new NotImplementedException();
        }

        public void Update(Laboratoire item)
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

        public IEnumerable<Laboratoire> Find(Func<Laboratoire, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Laboratoire FindSingle(Func<Laboratoire, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Laboratoire> GetAllLazyLoad(params Expression<Func<Laboratoire, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Laboratoire, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
