using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Commandes
{
    public class ServiceCommande:IServiceCommandes
    {
        private readonly IRepository<Commande> _repository;
        private readonly IRepository<LigneCommande> _repositoryLigne;

        public ServiceCommande(IRepository<Commande> repository , IRepository<LigneCommande> repositoryLigne  )
        {
            _repository = repository;
            _repositoryLigne = repositoryLigne;
        }

        public IEnumerable<Commande> Liste(long id)
        {
            return _repository.Find(x => x.ClientId == id);
        }

        public bool Insert(Commande commande)
        {
            try
            {
                _repository.Insert(commande);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insert(Commande commande, out long id)
        {
            try
            {
                _repository.Insert(commande);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                id = commande.Id;
            }
        }

        public bool Update(Commande commande)
        {
            try
            {
                _repository.Update(commande);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Commande FindSingle(long id)
        {
            return _repository.FindSingle(x => x.Id == id);
        }

        public LigneCommande GetSingleLigne(long id)
        {
            return _repositoryLigne.SelectById(id);
        }        
        public bool Delete(long id)
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
