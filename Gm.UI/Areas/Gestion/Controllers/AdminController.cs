using System;
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
        private  int _count;

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
            _count = _serviceUtilisateur.NonActiveUsers().Count();
        }

        // GET: Gestion/Admin
        public ActionResult Index()
        {
            Session["NewUsers"] = _count;
            return View();

        }
        [HttpPost]

        public ActionResult Notifications()
        {
            var count1 = _serviceUtilisateur.NonActiveUsers().Count();
            return count1 >= _count ? Json(count1, JsonRequestBehavior.AllowGet) : Json(null);
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
            //_count = _serviceUtilisateur.NonActiveUsers().Count();
            var data2 = new
            {
                message =(!b)? SuccessMessage():ErrorMessage(),
                data =_serviceUtilisateur.NonActiveUsers().Count()
            };
           return Json(data2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            Guid? identity = new Guid(id.Trim());
            var b = string.IsNullOrEmpty(id) || !_service.AccepteInscription(identity);
            _count = _serviceUtilisateur.NonActiveUsers().Count();
            var data2 = new
            {
                message = (!b) ? SuccessMessage() : ErrorMessage(),
                data = _count
            };
            return Json(data2,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Desactive(string id)
        {
            Guid? identity = new Guid(id.Trim());
            var b = string.IsNullOrEmpty(id) || !_service.DesactiveCompte(identity);
            _count = _serviceUtilisateur.NonActiveUsers().Count();
            var data2 = new
            {
                message = (!b) ? SuccessMessage() : ErrorMessage(),
                data = _count
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
            var user = new Utilisateur
            {
                Pseudo = pseudo,
                Email = email
            };
            var result = _serviceUtilisateur.FilterListe(user);
            var filter = Session["filter"].ToString();

            switch (filter)
            {

                case "2":
                {
                    return Json(_serviceUtilisateur.FilterByActivation(result, true).ToDataSourceResult(request));
                }
                case "3":
                {
                    return Json(_serviceUtilisateur.FilterByActivation(result, false).ToDataSourceResult(request));
                }

            }
            return Json(result.ToDataSourceResult(request));

        }

        private static string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }

        public string SuccessMessage()
        {
            return "<div class='alert alert-info'><p>l'operation est terminée avec succés!</p><div/>";
        }
    }
}