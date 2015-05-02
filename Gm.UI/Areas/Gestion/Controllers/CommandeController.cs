using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GM.Core.Models;
using GM.Services.Medicaments;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using GM.Services.Commandes;
using GM.Services.Fournisseurs;
using Gm.UI.Areas.Gestion.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "pharmacien,distributeur")]
    public class CommandeController : Controller
    {
        private readonly IServiceCommandes _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceMedicmaent _serviceMedicmaent;
        private readonly IEnumerable<Fournisseur> _liste; 

        public CommandeController(IServiceCommandes service, 
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur, 
            IServiceFournisseur serviceFournisseur , 
            IServiceMedicmaent serviceMedicmaent)
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceMedicmaent = serviceMedicmaent;
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
        public ActionResult GetList( DataSourceRequest request)
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
            if (Session["entreprise"] == null)
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            var model = new Commande();
            if (User.IsInRole("pharmacien")) model.ClientId = Convert.ToInt32(Session["entreprise"]);
            else
            {
                model.FournisseurId = Convert.ToInt32(Session["entreprise"]);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Commande model , bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom" , model.FournisseurId);
            if (ModelState.IsValid)
            {
                long identity;
                _service.Insert(model , out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id = identity;
            }
            if (Session["entreprise"] == null)
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            if (User.IsInRole("pharmacien")) model.ClientId = Convert.ToInt32(Session["entreprise"]);
            
            
            return (continuer) ? View(model) : View("Index");
        }
        public ActionResult DetailCommade(long? id)
        {
            if (id == null || id == 0) return PartialView("_CreateOrUpdateLigne" , new LigneCommande());
            var ligne = _service.GetSingleLigne(Convert.ToInt64(id));
            return PartialView("_CreateOrUpdateLigne", ligne);
        }
        [HttpPost]
        public ActionResult SearchMedicament()
        {
            var name = Request["q"];
            if (string.IsNullOrWhiteSpace(name)) return Json(new List<Medicament>(), JsonRequestBehavior.AllowGet);
            var dci = new Dci
            {
                Nom = name
            };
            var filter = new Medicament
            {
                NomCommerciale = name,
                Code = name,
                NumEnregistrement = name,
                Dci = dci, 
                Dose = name
               // LaboratoireId = Convert.ToInt32(labId)
            };
            var list = Mapper.Map<IList<MedicamentModel>>(_serviceMedicmaent.AutoCOmpleteListe(filter));
           
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}