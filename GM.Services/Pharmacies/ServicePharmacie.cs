using System;
using System.Collections.Generic;
using System.Linq;
using EntyTea.EntityQueries;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Pharmacies
{
    public class ServicePharmacie:IServicePharmacie
    {
        private readonly IRepository<Pharmacie> _repository;

        public ServicePharmacie(IRepository<Pharmacie>repository )
        {
            _repository = repository;
        }

        public int? GetPharmacie(Guid id)
        {
            var item = _repository.FindSingle(x => x.PropreitaireId == id);
            return (item!= null)? _repository.FindSingle(x => x.PropreitaireId == id).Id:0;
        }

        public bool Insert(Pharmacie pharmacie)
        {
            try
            {
                _repository.Insert(pharmacie);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Pharmacie pharmacie)
        {
            try
            {
                _repository.Update(pharmacie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Existe(Guid id)
        {
            return _repository.Exist(x=>x.PropreitaireId==id);
        }

        public Pharmacie SinglePharmacie(int id)
        {
            return _repository.SelectById(id);
        }

        public IEnumerable<Pharmacie> GetListe()
        {
           return _repository.SelectAll();
        }

        public IEnumerable<Pharmacie> SearchResult(Pharmacie fournisseur)
        {
            var filter = from m in EntityFilter<Pharmacie>.AsQueryable()
                         where m.Nom.Contains(fournisseur.Nom) || string.IsNullOrEmpty(fournisseur.Nom)
                               || m.Wilaya.Contains(fournisseur.Wilaya) || string.IsNullOrEmpty(fournisseur.Wilaya)
                               || m.Wilaya.Contains(fournisseur.Commune) || string.IsNullOrEmpty(fournisseur.Commune)
                               || m.Wilaya.Contains(fournisseur.Tel) || string.IsNullOrEmpty(fournisseur.Tel)
                               || m.Wilaya.Contains(fournisseur.Email) || string.IsNullOrEmpty(fournisseur.Email)
                         select m;
            return filter.Filter(_repository.SelectAll().AsQueryable());
        }
    }
}
