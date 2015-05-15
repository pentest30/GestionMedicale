using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Fournisseurs;
using GM.Services.Magasins;
using GM.Services.Medicaments;
using GM.Services.Pharmacies;
using GM.Services.Sorties;
using GM.Services.Stocks;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class SortieController : Controller
    {
        private readonly IServiceMagasin _serviceMagasin;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceFournisseur _serviceFournisseur;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceMedicmaent _serviceMedicmaent;
        private readonly IServiceSortie _service;
        private readonly IServiceStock _serviceStock;
        private readonly IEnumerable<Fournisseur> _liste;

        public SortieController(IServiceMagasin serviceMagasin,
            IServiceUtilisateur serviceUtilisateur,
            IServiceFournisseur serviceFournisseur,
            IServicePharmacie servicePharmacie,
            IServiceMedicmaent serviceMedicmaent,
            IServiceSortie service,
            IServiceStock serviceStock)
        {
            _serviceMagasin = serviceMagasin;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
            _servicePharmacie = servicePharmacie;
            _serviceMedicmaent = serviceMedicmaent;
            _service = service;
            _serviceStock = serviceStock;
            _liste = _serviceFournisseur.AllFournisseurs();
        }

        // GET: Gestion/Sortie
        public ActionResult Index()
        {
            Session["entreprise"] = null;
            GetEntrepriseId();
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(Convert.ToInt32(Session["entreprise"])), "Id",
              "Libelle");
            return View();
        }
        public ActionResult GetList(DataSourceRequest request)
        {
            return Json(_service.Liste(Convert.ToInt32(Convert.ToInt32(Session["entreprise"]))).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            GetEntrepriseId();
            ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(Convert.ToInt32(Session["entreprise"])), "Id",
                "Libelle");
            var model = new Sortie
            {
                ClientId = Convert.ToInt32(Session["entreprise"])
            };
            return View(model);
        }
        public ActionResult Update(long? id)
        {
            if (id == null) return HttpNotFound();
            var model = _service.FindSingle(Convert.ToInt64(id));
            ViewData["id"] = model.Id;
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            return View(model);
        }
        public ActionResult Create(Sortie model, bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            GetEntrepriseId();
            if (ModelState.IsValid)
            {
                long identity;
                _service.Insert(model, out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id = identity;
            }
            model.ClientId = Convert.ToInt32(Session["entreprise"]);

            return (continuer) ? View(model) : View("Index");
        }
        [HttpPost]
        public ActionResult Update(Sortie model, bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            GetEntrepriseId();
            if (ModelState.IsValid)
            {
                //long identity;
                _service.Update(model);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id;
            }
            model.ClientId = Convert.ToInt32(Session["entreprise"]);

            return (continuer) ? View(model) : View("Index");
        }
        public ActionResult Delete(int? id)
        {
            var b = id != null && _service.Delete((int)id);
            var data = new
            {
                message = (b) ? SuccessMessage() : ErrorMessage(),
                //data = _serviceUtilisateur.NonActiveUsers().Count()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private void GetEntrepriseId()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            if (User.IsInRole("pharmacien"))
            {
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            else if (User.IsInRole("distributeur"))
            {
                Session["entreprise"] = Convert.ToInt32(_serviceFournisseur.GetFournisseur(user.Id));
            }

        }
        private string SuccessMessage()
        {
            return "<div class='alert alert-info'><p>l'operation est terminée avec succés!</p><div/>";
        }
        private string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }
    }
}