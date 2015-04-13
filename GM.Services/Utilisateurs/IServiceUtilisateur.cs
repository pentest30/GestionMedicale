using System;
using System.Collections.Generic;
using GM.Core;
using GM.Core.Models;

namespace GM.Services.Utilisateurs
{
    public interface IServiceUtilisateur
    {
       
        IEnumerable<Role> SelectRoles();
        bool ExisteDeja(string identifiant);
        Role GetSingleRole(Guid? id);
        bool Authentification(Utilisateur utilisateur , string password , bool remember);
        Utilisateur VoirProfile(Guid? id);
        bool ModifierProfile(Utilisateur utilisateur);
        bool Inscription(Utilisateur utilisateur , string password , int ?[] roles);
        IEnumerable<Utilisateur> AllUsers();
        IEnumerable<Utilisateur> ActiveUsers();
        IEnumerable<Utilisateur> NonActiveUsers();
        Utilisateur SingleUser(string identifiant);
        string Role( Utilisateur item);
        void Logout();
        IEnumerable<Utilisateur> FlietrByPseudo(string identifiant);
        IEnumerable<Utilisateur> FlietrByEmail(string identifiant);
    }
}