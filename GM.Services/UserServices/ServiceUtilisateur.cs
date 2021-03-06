﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using GM.Core.Models;
using System.Web.Helpers;
using GM.Core;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace GM.Services.UserServices
{
    

    public class ServiceUtilisateur :  IServiceUtilisateur
    {
        private readonly IRepository<Utilisateur> _repository;
        private readonly IRepository<UtilisateurRole> _roleUserRepository;
        private readonly IRepository<Role> _roleRepository;


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public ServiceUtilisateur(IRepository<Utilisateur> repository, IRepository<UtilisateurRole> roleUserRepository, IRepository<Role> roleRepository)
        {
            _repository = repository;
            _roleUserRepository = roleUserRepository;
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> SelectRoles()
        {
           return _roleRepository.Find(x=>x.Public);
        }

        public Role GetSingleRole(string id)
        {
            return _roleRepository.FindSingle(x=>x.Nom==id);
        }


        public Role GetSingleRole(Guid? id)
        {
            throw new NotImplementedException();
        }

        public bool Authentification(Utilisateur utilisateur , string password , bool remember)
        {
            var b =_repository.Exist(x => x.Email.Equals(utilisateur.Email) || x.Pseudo.Equals(utilisateur.Pseudo));
            if (!b || !Crypto.VerifyHashedPassword(utilisateur.PasswordHash, password)) return false;
            Authenticate(utilisateur, remember);
            return true;
        }

        public bool ExisteDeja(string identifiant)
        {
            return _repository.Exist(x => x.Email.Equals(identifiant) || x.Pseudo.Equals(identifiant));
        }

        public Utilisateur VoirProfile(Guid? id)
        {
            var item = _repository.SelectById(id);
            item.UtilisateurRoles = _roleUserRepository.Find(x => x.UtilisateurId == id).ToList();
            return item;
        }

        public bool ModifierProfile(Utilisateur utilisateur)
        {
            try
            {
                _repository.Update(utilisateur);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void Authenticate(Utilisateur result, bool remember)
        {
            if (result == null) return;
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, result.Pseudo)
            },
                DefaultAuthenticationTypes.ApplicationCookie,
                ClaimTypes.Name, ClaimTypes.Role);
            var list = _roleUserRepository.Find(x => x.UtilisateurId == result.Id);

            // if you want roles, just add as many as you want here (for loop maybe?)
            foreach (var userRole in list.Select(x=>x.Roles))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, userRole.Nom.ToLower()));
            }

            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = remember }, identity);
        }
        public bool Inscription(Utilisateur utilisateur , string password , int ?[] roles)
        {
            try
            {
                //utilisateur.Id = Guid.NewGuid();
                utilisateur.PasswordHash = Crypto.HashPassword(password);
                _repository.Insert(utilisateur);
                foreach (var role in roles.Select(r => new UtilisateurRole
                {
                    UtilisateurId = utilisateur.Id,
                    RoleId = Convert.ToInt32(r)
                }))
                {
                    _roleUserRepository.Insert(role);
                }
               // Authentification(utilisateur, password, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Utilisateur> AllUsers()
        {
            return _repository.SelectAll();
        }

        public IEnumerable<Utilisateur> ActiveUsers()
        {
            return _repository.Find(x => x.Validation);
        }

        public IEnumerable<Utilisateur> NonActiveUsers()
        {
            return _repository.Find(x => !x.Validation);
        }

        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
