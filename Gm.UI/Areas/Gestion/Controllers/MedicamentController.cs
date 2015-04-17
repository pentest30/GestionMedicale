using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using GM.Core.Models;
using GM.Services.Categorie;
using GM.Services.Conditionnelts;
using GM.Services.Fabriquant;
using GM.Services.Formes;
using GM.Services.Medicaments;
using GM.Services.Nomenclature;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly IServiceMedicmaent _service;
        private readonly IServiceSpecialite _serviceSpecialite;
        private readonly IServiceDci _serviceDci;
        private readonly IServiceForme _serviceForme;
        private readonly IServiceConditionnement _serviceConditionnement;
        private readonly IServiceFabriquant _serviceFabriquant;
        // GET: Gestion/Medicament
        public MedicamentController(IServiceMedicmaent service,
            IServiceSpecialite serviceSpecialite , 
            IServiceDci serviceDci ,IServiceForme serviceForme , 
            IServiceConditionnement serviceConditionnement ,
            IServiceFabriquant serviceFabriquant)
        {
            _service = service;
            _serviceSpecialite = serviceSpecialite;
            _serviceDci = serviceDci;
            _serviceForme = serviceForme;
            _serviceConditionnement = serviceConditionnement;
            _serviceFabriquant = serviceFabriquant;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            ViewData["formes"] = new SelectList(_serviceForme.ListeFormes(), "Id", "Libelle");
            ViewData["conditionnements"] = new SelectList(_serviceConditionnement.Liste(), "Id", "Libelle");
            ViewData["fabriquants"] = new SelectList(_serviceFabriquant.Liste(), "Id", "Libelle");
            return View();
        }
        [HttpGet]
        public ActionResult Update(int ? id)
        {
            var model = Mapper.Map<MedicamentModel>(_service.FindSingle(Convert.ToInt32(id)));
            ViewData["id"] = model.Id;
            InitDrops(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicamentModel model , bool continuer)
        {
            if (ModelState.IsValid)
            {
                int identity;
                var medicament = Mapper.Map<Medicament>(model);
                _service.Insert(medicament, out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id = identity;
            }
            InitDrops(model);
            return (continuer)? View(model):View("Index");
        }

        private void InitDrops(MedicamentModel model)
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle", model.SpecialiteId);
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom", model.DciId);
            ViewData["formes"] = new SelectList(_serviceForme.ListeFormes(), "Id", "Libelle", model.FormeId);
            ViewData["conditionnements"] = new SelectList(_serviceConditionnement.Liste(), "Id", "Libelle",
                model.ConditionnementId);
            ViewData["fabriquants"] = new SelectList(_serviceFabriquant.Liste(), "Id", "Libelle", model.LaboratoireId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Update(MedicamentModel model, bool continuer)
        {

            ViewData["id"] = model.Id ;
            ModelState.Remove("NomCommerciale");
            if (ModelState.IsValid)
            {
                
                var medicament = Mapper.Map<Medicament>(model);
                _service.Update(medicament);
                ViewData["info"] = "Opération est terminé avec succéss !";
            }
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle", model.SpecialiteId);
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom", model.DciId);
            ViewData["formes"] = new SelectList(_serviceForme.ListeFormes(), "Id", "Libelle", model.FormeId);
            ViewData["conditionnements"] = new SelectList(_serviceConditionnement.Liste(), "Id", "Libelle", model.ConditionnementId);
            ViewData["fabriquants"] = new SelectList(_serviceFabriquant.Liste(), "Id", "Libelle", model.LaboratoireId);
            return (continuer) ? View(model) : View("Index");
        }
        //[ChildActionOnly]
        [HttpPost]
        public ActionResult CreateRemboussement(Remboursement model)
        {
           // if (!ModelState.IsValid) return null;
            var b = _service.InsertRemboussement(model);
            if (Request.IsAjaxRequest())
            {
                dynamic data = new
                {
                    message = b ? SuccessMessage() : ErrorMessage()
                };
                return Json(data, JsonRequestBehavior.AllowGet);
               
            }
            return null;

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateRembousement([DataSourceRequest] DataSourceRequest request, Remboursement product)
        {
            if (product != null && ModelState.IsValid)
            {
                _service.UpdateRemboussement(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Index()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            return View();
        }

        public ActionResult GetListeRemb([DataSourceRequest] DataSourceRequest request,int? id)
        {
            return Json(_service.GetListRemboursements(id).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ListeMedicaments([DataSourceRequest] DataSourceRequest request, int?  specialiteId , int? dciId , string nom, string code , string nEnregistrement , int? labId)
        {
            var filter = new Medicament
            {
                SpecialiteId =Convert.ToInt32( specialiteId),
                DciId = Convert.ToInt32(dciId),
                NomCommerciale = nom,
                Code = code,
                NumEnregistrement = nEnregistrement
            };
            var list = Mapper.Map<IList<MedicamentModel>>(_service.FilterListe(filter));
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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