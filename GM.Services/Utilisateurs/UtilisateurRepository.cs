using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GM.Context;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Utilisateurs
{
   public class UtilisateurRepository:IRepository<Utilisateur>
    {
        private readonly UserContext _db;

        public UtilisateurRepository(UserContext db)
        {
            _db = db;
        }

        public IEnumerable<Utilisateur> SelectAll()
        {
            return _db.Utilisateurs.Include(x=>x.UtilisateurRoles).ToList();
        }

       public bool Exist(Func<Utilisateur, bool> predicate)
       {
           return _db.Utilisateurs.Any(predicate);
       }
        public Utilisateur SelectById(object id)
        {
            var item = _db.Utilisateurs.Find(id);
            return item ?? new Utilisateur();
        }

        public void Insert(Utilisateur item)
        {
            _db.Utilisateurs.Add(item);
            Save();
        }

        public void Update(Utilisateur item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Utilisateurs.Find(id);
            if (item == null) return;
            _db.Entry(item).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Utilisateur> Find(Func<Utilisateur, bool> predicate)
        {
            return _db.Utilisateurs.Include(x=>x.UtilisateurRoles).Where(predicate);
        }

        public Utilisateur FindSingle(Func<Utilisateur, bool> predicate)
        {
            return _db.Utilisateurs.FirstOrDefault(predicate);
        }

        public IEnumerable<Utilisateur> GetAllLazyLoad(params Expression<Func<Utilisateur, object>>[] children)
        {
            throw new NotImplementedException();
        }

       
    }
}
