using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GM.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceAdministrateur _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IDictionary<int, string> _filtreUsers;


        public AdminController(IServiceAdministrateur service, IServiceUtilisateur serviceUtilisateur )
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
            _filtreUsers = new Dictionary<int, string>();
            _filtreUsers.Add(1,"Touts les utilisateurs");
            _filtreUsers.Add(2, "Utilisateurs activés");
            _filtreUsers.Add(3, "Utilisateurs non activés");
        }

        // GET: Gestion/Admin
        public ActionResult Index()
        {
            
            return View();

        }

        public ActionResult Utilisateurs()
        {
            ViewData["filterUsers"] = new SelectList(_filtreUsers, "Key", "Value");
            return View();
        }

        [HttpPost]
        //[filte(false)]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<div class='alert alert-danger'><p>Suppression est terminer avec succés!</p><div/>");

            Guid? identity = new Guid(id.Trim());
            _service.SupprimeCompte(identity);
            return Content("<div class='alert alert-success'><p>Suppression est terminer avec succés!</p><div/>");
        }
         [HttpPost]
        public ActionResult Activate(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<div class='alert alert-danger'><p>Suppression est terminer avec succés!</p><div/>");

            Guid? identity = new Guid(id.Trim());
            _service.AccepteInscription(identity);
            return Content("<div class='alert alert-info'><p>Activation est terminer avec succés!</p><div/>");
        }

        public ActionResult AllUsers([DataSourceRequest] DataSourceRequest request, int ? filter)
        {
            if (filter != null)
            {
                switch (filter)
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
            }
            return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
        }
    }
}