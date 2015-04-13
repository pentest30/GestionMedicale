﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Models.Utilisateurs;
using GM.Core;
using GM.Core.Models;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceAdministrateur _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IDictionary<int, string> _filtreUsers;
        private readonly IList<Role> _roles;

        public AdminController(IServiceAdministrateur service, IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
            _roles = new List<Role>
            {
                new Role
                {
                    Id = 0,
                    Nom = ""
                }
            };
            _roles.AddRange(_serviceUtilisateur.SelectRoles());
            _filtreUsers = new Dictionary<int, string>
            {
                {1, "Tous les utilisateurs"},
                {2, "Comptes actifs"},
                {3, "comptes non actifs"}
            };
        }

        // GET: Gestion/Admin
        public ActionResult Index()
        {
            Session["NewUsers"] = _serviceUtilisateur.NonActiveUsers().Count();
            return View();

        }
        [HttpPost]
        //[ChildActionOnly]
        public ActionResult UserDetails(string id)
        {
            if (id == "") return HttpNotFound();
            var identity = new Guid(id.Trim());
            var user =_serviceUtilisateur.VoirProfile(identity);
            var model = Mapper.Map<RegisterModel>(user); 
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
            return View(model);
        }
        [HttpGet]
        public ActionResult Utilisateurs()
        {
            Session["filter"] = 0;
            ViewData["filterUsers"] = new SelectList(_filtreUsers, "Key", "Value");
            ViewData["roles"] = new SelectList(_roles, "Id", "Nom");
            Session["NewUsers"] = _serviceUtilisateur.NonActiveUsers().Count();
            return View();
        }

        [HttpPost]
        //[filte(false)]
        public ActionResult Delete(string id)
        {
            Guid? identity = new Guid(id.Trim());
            var b = string.IsNullOrEmpty(id) || !_service.SupprimeCompte(identity);
            var data2 = new
            {
                message =(!b)? SuccessMessage():ErrorMessage(),
                data = _serviceUtilisateur.NonActiveUsers().Count()
            };
           return Json(data2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            Guid? identity = new Guid(id.Trim());
            var b = string.IsNullOrEmpty(id) || !_service.AccepteInscription(identity);
            var data2 = new
            {
                message = (!b) ? SuccessMessage() : ErrorMessage(),
                data = _serviceUtilisateur.NonActiveUsers().Count()
            };
            return Json(data2,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Desactive(string id)
        {
            Guid? identity = new Guid(id.Trim());
            var b = string.IsNullOrEmpty(id) || !_service.DesactiveCompte(identity);
            var data2 = new
            {
                message = (!b) ? SuccessMessage() : ErrorMessage(),
                data = _serviceUtilisateur.NonActiveUsers().Count()
            };
            return Json(data2, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateFilter(int? f)
        {
            Session["filter"] = Convert.ToInt32(f);
            return null;
        }

        public ActionResult AllUsers([DataSourceRequest] DataSourceRequest request, string pseudo, string email)
        {

            if (!string.IsNullOrEmpty(pseudo) || !string.IsNullOrEmpty(email))
            {
                var result = !string.IsNullOrEmpty(pseudo)?
                    _serviceUtilisateur.FlietrByPseudo(pseudo):
                    _serviceUtilisateur.FlietrByEmail(email);
                return Json(result.ToDataSourceResult(request));
            }
            var filter = Session["filter"].ToString();

            switch (filter)
            {

                case "2":
                {
                    return Json(_serviceUtilisateur.ActiveUsers().ToDataSourceResult(request));
                }
                case "3":
                {
                    return Json(_serviceUtilisateur.NonActiveUsers().ToDataSourceResult(request));
                }

            }
            return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
        }

        private static string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }

        public string SuccessMessage()
        {
            return "<div class='alert alert-info'><p>l'operation est terminée avec succés!</p><div/>";
        }
        private IEnumerable<Utilisateur> Utilisateurs(RegisterModel model)
        {
            var result = new List<Utilisateur>();
            if (!string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Pseudo))
                result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Email.Equals(model.Email)));
            else if (string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Pseudo))
                result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Pseudo == model.Pseudo));
            else if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Pseudo)) result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Pseudo == model.Pseudo && x.Email == model.Email));
            return result;
        }

    }
}