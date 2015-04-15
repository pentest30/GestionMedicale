using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gm.UI.Areas.Gestion.Models;
using GM.Core.Models;
using GM.Services.Categorie;
using GM.Services.Fabriquant;
using GM.Services.Conditionnelts;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicamentModel model , bool continuer)
        {
            if (ModelState.IsValid)
            {
                int identity;
                var medicament = AutoMapper.Mapper.Map<Medicament>(model);
                _service.Insert(medicament,out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                model.Id = identity;
            }
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle" ,model.SpecialiteId);
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom", model.DciId);
            ViewData["formes"] = new SelectList(_serviceForme.ListeFormes(), "Id", "Libelle", model.FormeId);
            ViewData["conditionnements"] = new SelectList(_serviceConditionnement.Liste(), "Id", "Libelle", model.ConditionnementId);
            ViewData["fabriquants"] = new SelectList(_serviceFabriquant.Liste(), "Id", "Libelle", model.LaboratoireId);
            return (continuer)? View(model):View("Index");
        }
        public ActionResult Index()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            return View();
        }

        [HttpPost]
        public JsonResult ExisteResult(string nomCommerciale)
        {
            return Json(_service.Existe(nomCommerciale), JsonRequestBehavior.AllowGet);
            
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
            var list = AutoMapper.Mapper.Map<IList<MedicamentModel>>(_service.FilterListe(filter));
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
 
    }
}