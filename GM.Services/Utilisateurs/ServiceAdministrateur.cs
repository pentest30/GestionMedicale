using System;
using System.Collections.Generic;
using System.Linq;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Utilisateurs
{
    public class ServiceAdministrateur : IServiceAdministrateur
    {
        private readonly IRepository<Utilisateur> _repository;
        private readonly IRepository<UtilisateurRole> _userRoleRepository;

        public ServiceAdministrateur(IRepository<Utilisateur> repository , IRepository<UtilisateurRole> userRoleRepository )
        {
            _repository = repository;
            _userRoleRepository = userRoleRepository;
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
            try
            {
                var arry = item.UtilisateurRoles.ToArray();
                foreach (var utilisateurRole in arry)
                {
                    _userRoleRepository.Delete(utilisateurRole.Id);
                    item.UtilisateurRoles.Remove(utilisateurRole);

                }
                _repository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
