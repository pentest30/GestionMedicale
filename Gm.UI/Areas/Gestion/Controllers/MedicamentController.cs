using System.Web.Mvc;
using GM.Services.Categorie;
using GM.Services.Conditionnelts;
using GM.Services.Formes;
using GM.Services.Medicaments;
using GM.Services.Nomenclature;

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

        public ActionResult Index()
        {
            ViewData["specialites"] = new SelectList(_serviceSpecialite.ListeSpecialites(), "Id", "Libelle");
            ViewData["dcis"] = new SelectList(_serviceDci.ListeDcis(), "Id", "Nom");
            return View();
        }
    }
}