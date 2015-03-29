using System;
using System.Collections.Generic;
using GM.Core.Models;

namespace GM.Core
{
    public interface IServiceUtilisateur
    {
        IEnumerable<Role> SelectRoles();
        bool ExisteDeja(string identifiant);
        Role GetSingleRole(string id);
        bool Authentification(Utilisateur utilisateur , string password , bool remember);
        Utilisateur VoirProfile(Guid? id);
        bool ModifierProfile(Utilisateur utilisateur);
        bool Inscription(Utilisateur utilisateur , string password , int ?[] roles);
        IEnumerable<Utilisateur> AllUsers();
    }
}