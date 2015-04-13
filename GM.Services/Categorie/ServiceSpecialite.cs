using System;
using System.Collections.Generic;
using System.Linq;
using EntyTea.EntityQueries;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Categorie
{
    public class ServiceSpecialite : IServiceSpecialite
    {
        private readonly IRepository<Specialite> _repository;

        public ServiceSpecialite(IRepository<Specialite> repository )
        {
            _repository = repository;
        }

        public IEnumerable<Specialite> ListeSpecialites()
        {
            return _repository.SelectAll();
        }

        public bool Insert(Specialite specialite)
        {
            try
            {
                _repository.Insert(specialite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Specialite specialite)
        {
            try
            {
                _repository.Update(specialite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Specialite FindSingle(int id)
        {
            return _repository.SelectById(id);
        }

        public bool Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Specialite> FilterListe(Specialite specialite)
        {
            var filter = from m in EntityFilter<Specialite>.AsQueryable()
                where m.Libelle.Equals(specialite.Libelle, StringComparison.InvariantCultureIgnoreCase) ||
                    string.IsNullOrEmpty(specialite.Libelle)
                where m.Code.Equals(specialite.Code) || string.IsNullOrEmpty(specialite.Code)
                select m;

            return filter.Filter(_repository.SelectAll().AsQueryable());
        }
    }
}
