using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Core
{
    public interface IServiceAdministrateur
    {
        bool AccepteInscription(Guid? id);
        bool SupprimeInscription(Guid? id);
        IEnumerable<Utilisateur> UtilisateursNonActive();
    }
}