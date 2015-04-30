using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Commandes
{
    public class ServiceCommande:IServiceCommandes
    {
        private readonly IRepository<Commande> _repository;

        public ServiceCommande(IRepository<Commande> repository )
        {
            _repository = repository;
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
