using System;
using System.Collections.Generic;
using System.Linq;
using EntyTea.EntityQueries;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Fournisseurs
{
    public class ServiceFournisseur:IServiceFournisseur
    {
        private readonly IRepository<Fournisseur> _repository;

        public ServiceFournisseur(IRepository<Fournisseur> repository )
        {
            _repository = repository;
        }

        public int? GetFournisseur(Guid id)
        {
            var item = _repository.FindSingle(x => x.PropreitaireId == id);
            return (item != null) ? _repository.FindSingle(x => x.PropreitaireId == id).Id : 0;
        }

        public bool Insert(Fournisseur fournisseur)
        {
            try
            {
                _repository.Insert(fournisseur);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Fournisseur fournisseur)
        {
            try
            {
                _repository.Update(fournisseur);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Existe(Guid id)
        {
            return _repository.Exist(x => x.PropreitaireId == id);
        }

        public Fournisseur SingleFournisseur(int id)
        {
            return _repository.SelectById(id);
        }

        public IEnumerable<Fournisseur> FounisseurNonInscript()
        {
            return _repository.Find(x =>x.PropreitaireId == null);
        }

        public IEnumerable<Fournisseur> AllFournisseurs()
        {
            return _repository.SelectAll();
        }

        public IEnumerable<Fournisseur> FounisseurInscript()
        {
            return _repository.Find(fournisseur => fournisseur.PropreitaireId.HasValue).ToList();
        }

        public IEnumerable<Fournisseur> SearchResult(Fournisseur fournisseur)
        {
            var filter = from m in EntityFilter<Fournisseur>.AsQueryable()
                         where m.Nom.Contains(fournisseur.Nom) || string.IsNullOrEmpty(fournisseur.Nom)
                      || m.Wilaya.Contains(fournisseur.Wilaya) || string.IsNullOrEmpty(fournisseur.Wilaya)
                      || m.Wilaya.Contains(fournisseur.Commune) || string.IsNullOrEmpty(fournisseur.Commune)
                      || m.Wilaya.Contains(fournisseur.Tel) || string.IsNullOrEmpty(fournisseur.Tel)
                      || m.Wilaya.Contains(fournisseur.Email) || string.IsNullOrEmpty(fournisseur.Email)
                select m;
            return filter.Filter(FounisseurInscript().AsQueryable());

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
    }
}
