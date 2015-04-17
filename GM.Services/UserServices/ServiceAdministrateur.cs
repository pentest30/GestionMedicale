using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.UserServices
{
    public class ServiceAdministrateur : IServiceAdministrateur
    {
        private readonly IRepository<Utilisateur> _repository;

        public ServiceAdministrateur(IRepository<Utilisateur> repository)
        {
            _repository = repository;
        }

        public bool AccepteInscription(Guid? id)
        {
            var item = _repository.SelectById(id);
            if (item == null) return false;
            item.Validation = true;
            _repository.Update(item);
            return true;
        }

        public bool SupprimeCompte(Guid? id)
        {
            var item = _repository.SelectById(id);
            if (item == null) return false;
            _repository.Delete(id);
            return true;
        }

        public IEnumerable<Utilisateur> UtilisateursNonActive()
        {
            return _repository.Find(x => !x.Validation);
        }

        public bool DesactiveCompte(Guid? id)
        {
            var item = _repository.SelectById(id);
            if (item == null) return false;
            item.Validation = false;
            _repository.Update(item);
            return true;
        }
    }
}
