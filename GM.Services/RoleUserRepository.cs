using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services
{
    public class RoleUserRepository:IRepository<UtilisateurRole>
    {
        private readonly UserContext _db;

        public RoleUserRepository(UserContext db)
        {
            _db = db;
        }

        public IEnumerable<UtilisateurRole> SelectAll()
        {
            throw new NotImplementedException();
        }

        public UtilisateurRole SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(UtilisateurRole item)
        {
            _db.UtilisateurRoles.Add(item);
            Save();
        }

        public void Update(UtilisateurRole item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<UtilisateurRole> Find(Func<UtilisateurRole, bool> predicate)
        {
            return _db.UtilisateurRoles.Where(predicate);
        }

        public UtilisateurRole FindSingle(Func<UtilisateurRole, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UtilisateurRole> GetAllLazyLoad(params Expression<Func<UtilisateurRole, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<UtilisateurRole, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
