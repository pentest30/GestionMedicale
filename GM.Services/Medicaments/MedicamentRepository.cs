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
    public class MedicamentRepository:IRepository<Medicament>
    {
        private readonly PharmacieContext _db;

        public MedicamentRepository(PharmacieContext db)
        {
            _db = db;
        }

        public IEnumerable<Medicament> SelectAll()
        {
            return _db.Medicaments.
                Include("Remboursements").
                Include("ParamStocks").
                Include("Dci").
                Include("Specialite").
                Include("Forme").
                Include("Conditionnement").
                Include("Laboratoire");
        }

        public Medicament SelectById(object id)
        {
            var item = _db.Medicaments.Find(id);
            return item;
        }

        public void Insert(Medicament item)
        {
            _db.Medicaments.Add(item);
            Save();
        }

        public void Update(Medicament item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            var item = _db.Medicaments.Find(id);
            if (item != null)
            {
                _db.Entry(item ).State = EntityState.Deleted;
                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Medicament> Find(Func<Medicament, bool> predicate)
        {
            return _db.Medicaments.Where(predicate);
        }

        public Medicament FindSingle(Func<Medicament, bool> predicate)
        {
            return _db.Medicaments.  Include("ParamStocks").FirstOrDefault(predicate);
        }

       
        public IEnumerable<Medicament> GetAllLazyLoad(params Expression<Func<Medicament, object>>[] children)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Medicament, bool> predicate)
        {
            return _db.Medicaments.Any(predicate);
        }

        

    }
}
