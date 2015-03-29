using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Core
{
    public interface IServiceAdministrateur
    {
        bool AccepteInscription(Guid? id);
        bool SupprimeCompte(Guid? id);
        IEnumerable<Utilisateur> UtilisateursNonActive();
    }
}