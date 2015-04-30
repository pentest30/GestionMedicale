using System;
using System.Collections.Generic;
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

        public IEnumerable<Fournisseur> GeltList()
        {
            return _repository.SelectAll();
        }
    }
}
