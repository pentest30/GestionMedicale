using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using Gm.UI.Models.Docteurs;
using GM.Core.Models;
using GM.Services.Cabinets;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using MvcPaging;

namespace Gm.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceUtilisateur _service;
        private readonly IServiceCabinet _serviceCabinet;
        private readonly IServicePharmacie _servicePharmacie;

        public HomeController(IServiceUtilisateur service ,IServiceCabinet serviceCabinet,
            IServicePharmacie servicePharmacie )
        {
            _service = service;
            _serviceCabinet = serviceCabinet;
            _servicePharmacie = servicePharmacie;
        }

        public ActionResult Index()
        {
            Session["Roles"] = _service.SelectRoles();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //ViewData["Roles"] = _service.SelectRoles();
            return View();
        }
        [Authorize]
        public ActionResult Info()
        {
            ViewBag.Message = "Ce compte n'est pas encore activé , Merci!";
            //ViewData["Roles"] = _service.SelectRoles();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //ViewData["Roles"] = _service.SelectRoles();
            return View();
        }

        public ActionResult Pharmaciens()
        {
            return View();
        }
        public ActionResult Medecins()
        {
            return View();
        }

        public ActionResult GetMedecins(int? page, string filter, string param)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 1;
            var search = new DocteurModel();
            var result = _service.SelectMedecins("medecin");
            var finalresult = Mapper.Map<IList<DocteurModel>>(result);
            foreach (var medecin in finalresult) medecin.Cabinet = _serviceCabinet.SingleCabinet(medecin.Id);
            switch (param)
            {
                case "Nom de medecin":
                    {
                        search.Nom = filter;
                        return PartialView("_ListeMedecins", finalresult.Where(x => x.Nom.Contains(filter)).ToPagedList(currentPageIndex, 10));
                    }
                case "Nom de cabinet":
                    {
                        search.Nom = filter;
                        return PartialView("_ListeMedecins", finalresult.Where(x => x.Cabinet.Nom.Contains(filter)).ToPagedList(currentPageIndex, 10));
                    }
                default:
                {
                    return PartialView("_ListeMedecins", finalresult.ToPagedList(currentPageIndex, 10));
                }
               
            }
           
        }


        public ActionResult GetListPharmacies(int? page, string filter, string param)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 1;
            var search = new Pharmacie();

            switch (param)
            {
                case "Nom de pharmacie":
                {
                    search.Nom = filter;
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                }
                case "Wilaya":
                {
                    search.Wilaya = filter;
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                }
                case "Commune":
                {
                    search.Commune = filter;
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                }
                case "Tel":
                {
                    search.Tel = filter;
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                }
                case "Email":
                {
                    search.Email = filter;
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                }
                default:
                {
                    var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.GetListe());
                    return PartialView("_ListePharmacies", result.ToPagedList(currentPageIndex, 10));
                    
                }


            }
        }
    }
}