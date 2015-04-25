using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
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
        private readonly IServicePharmacie _pharmacieService;
        private readonly IServiceUtilisateur _utilisateurService;

        // GET: Gestion/Medicament
        public MedicamentController(IServiceMedicmaent service,
            IServiceSpecialite serviceSpecialite , 
            IServiceDci serviceDci ,IServiceForme serviceForme , 
            IServiceConditionnement serviceConditionnement ,
            IServiceFabriquant serviceFabriquant, 
            IServicePharmacie pharmacieService,
            IServiceUtilisateur utilisateurService)
        {
            _service = service;
            _serviceSpecialite = serviceSpecialite;
            _serviceDci = serviceDci;
            _serviceForme = serviceForme;
            _serviceConditionnement = serviceConditionnement;
            _serviceFabriquant = serviceFabriquant;
            _pharmacieService = pharmacieService;
            _utilisateurService = utilisateurService;
        }
        [HttpGet]
        public ActionResult Create()
        {
            var user = _utilisateurService.SingleUser(User.Identity.Name);
            ViewData["entrpriseId"] = _pharmacieService.GetPharmacie(user.Id);
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
            var user = _utilisateurService.SingleUser(User.Identity.Name);
            var enId = _pharmacieService.GetPharmacie(user.Id);
            ViewData["entrpriseId"] = enId;
            var med = _service.FindSingle(Convert.ToInt32(id));
            var model = Mapper.Map<MedicamentModel>(med);
            if (med.ParamStocks.Count > 0)
                model.ParamStock = _service.GetParamStock(med, Convert.ToInt32(enId));
            ViewData["id"] = model.Id;
            InitDrops(model);
            return View(model);
        }

    

        [HttpPost]
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MedicamentModel model, bool continuer)
        {

            ViewData["id"] = model.Id ;
            ModelState.Remove("NomCommerciale");
            var user = _utilisateurService.SingleUser(User.Identity.Name);
            var enId = _pharmacieService.GetPharmacie(user.Id);
            ViewData["entrpriseId"] = enId;
            if (ModelState.IsValid)
            {

                var medicament = Mapper.Map<Medicament>(model);
                _service.Update(medicament);
                medicament = _service.FindSingle(Convert.ToInt32(model.Id));
                if (medicament.ParamStocks.Count > 0)
                    model.ParamStock = _service.GetParamStock(medicament, Convert.ToInt32(enId));
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

        public ActionResult CreateUpdateParams(ParamStock model)
        {
            bool b;
            if (model.ParmsId == 0)
            {
                b= _service.InsertParamsStock(model);
               dynamic data = new
               {
                   message = b ? SuccessMessage() : ErrorMessage()
               };
               return Json(data, JsonRequestBehavior.AllowGet);
            }
             b = _service.UpdateParamsStock(model);
            dynamic data2 = new
            {
                message = b ? SuccessMessage() : ErrorMessage()
            };
            return Json(data2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            ViewData["fabriquants"] = new SelectList(_serviceFabriquant.Liste(), "Id", "Libelle");
            ViewData["max"] = 0;
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
                NumEnregistrement = nEnregistrement,
                LaboratoireId =Convert.ToInt32( labId)
            };
            
            var list = Mapper.Map<IList<MedicamentModel>>(_service.FilterListe(filter));
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ImportXls()
        {
            
            HttpPostedFileBase file = Request.Files[0]; 
            if (file != null && file.ContentLength <= 0) return null;
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    _service.ImporteListe(SaveFile(fileName, file));
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("Index");
        }

        private string SaveFile(string fileName, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            file.SaveAs(path);
            return path;
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