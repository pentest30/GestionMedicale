using System;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Categorie;
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
        // GET: Gestion/Medicament
        public MedicamentController(IServiceMedicmaent service,IServiceSpecialite serviceSpecialite , IServiceDci serviceDci ,IServiceForme serviceForme , IServiceConditionnement serviceConditionnement)
        {
            _service = service;
            _serviceSpecialite = serviceSpecialite;
            _serviceDci = serviceDci;
            _serviceForme = serviceForme;
            _serviceConditionnement = serviceConditionnement;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            ViewData["formes"] = new SelectList(_serviceForme.ListeFormes(), "Id", "Nom");
            ViewData["conditionnements"] = new SelectList(_serviceConditionnement.Liste(), "Id", "Nom");
            return View();
        }

        public ActionResult Index()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            return View();
        }

        public ActionResult ListeMedicaments([DataSourceRequest] DataSourceRequest request, int?  specialiteId , int? dciId , string nom, string code , string nEnregistrement , int labId)
        {
            var filter = new Medicament()
            {
                SpecialiteId =Convert.ToInt32( specialiteId),
                DciId = Convert.ToInt32(dciId),
                NomCommerciale = nom,
                Code = code,
                NumEnregistrement = nEnregistrement
            };
            return Json(_service.FilterListe(filter).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}