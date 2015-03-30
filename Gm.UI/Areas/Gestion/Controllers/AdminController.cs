using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GM.Core;
using GM.Core.Models;
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
        private int _filter;


        public AdminController(IServiceAdministrateur service, IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
            _roles = new List<Role>();
            _roles.Add(new Role
            {
                Id = 0,
                Nom = ""
            });
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
            return View();

        }

        public ActionResult Utilisateurs()
        {
            Session["filter"] = 0;
            ViewData["filterUsers"] = new SelectList(_filtreUsers, "Key", "Value");
            ViewData["roles"] = new SelectList(_roles, "Id", "Nom");
            return View();
        }

        [HttpPost]
        //[filte(false)]
        public ActionResult Delete(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.SupprimeCompte(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.AccepteInscription(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());
        }

        [HttpPost]
        public ActionResult Desactive(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.DesactiveCompte(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());

        }

        [HttpPost]
        public ActionResult UpdateFilter(int? f)
        {
            Session["filter"] = Convert.ToInt32(f);
            return null;
        }

        public ActionResult AllUsers([DataSourceRequest] DataSourceRequest request)
        {

            _filter = Convert.ToInt32(Session["filter"].ToString());

            if (_filter == 0)
            {
                return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
            }
            switch (_filter)
            {
                case 1:
                {
                    return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
                }
                case 2:
                {
                    return Json(_serviceUtilisateur.ActiveUsers().ToDataSourceResult(request));
                }
                case 3:
                {
                    return Json(_serviceUtilisateur.NonActiveUsers().ToDataSourceResult(request));
                }

            }
            return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
        }

        private string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }

        private string SuccessMessage()
        {
            return "<div class='alert alert-success'><p>Suppression est terminer avec succés!</p><div/>";
        }

    }
}