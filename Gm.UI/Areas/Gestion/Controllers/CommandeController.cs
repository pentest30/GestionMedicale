using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using GM.Services.Commandes;
using GM.Services.Fournisseurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class CommandeController : Controller
    {
        private readonly IServiceCommandes _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IEnumerable<Fournisseur> _liste; 

        public CommandeController(IServiceCommandes service, 
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur, 
            IServiceFournisseur serviceFournisseur)
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            _liste = serviceFournisseur.GeltList();
        }

        // GET: Gestion/Commande
        public ActionResult Index()
        {
            if (Session["entreprise"] == null)
               
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            return View();
        }
        public ActionResult GetList([DataSourceRequest] DataSourceRequest request)
        {
            int? id;
            if (Session["entreprise"] != null)
                id = Convert.ToInt32(Session["entreprise"]);
            else
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                id = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }

            return Json(_service.Liste(Convert.ToInt32(id)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            return View();
        }

    }
}