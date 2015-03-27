using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services
{
   public class RoleRepository:IRepository<Role>
   {
       private readonly UserContext _db;

       public RoleRepository(UserContext db)
       {
           _db = db;
       }

       public IEnumerable<Role> SelectAll()
       {
           return _db.Roles;
       }

       public Role SelectById(object id)
       {
           throw new NotImplementedException();
       }

       public void Insert(Role item)
       {
           throw new NotImplementedException();
       }

       public void Update(Role item)
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

       public IEnumerable<Role> Find(Func<Role, bool> predicate)
       {
           return _db.Roles.Where(predicate);
       }

       public Role FindSingle(Func<Role, bool> predicate)
       {
           return _db.Roles.FirstOrDefault(predicate);
       }

       public IEnumerable<Role> GetAllLazyLoad(params Expression<Func<Role, object>>[] children)
       {
           throw new NotImplementedException();
       }

       public bool Exist(Func<Role, bool> predicate)
       {
           throw new NotImplementedException();
       }
   }
}
