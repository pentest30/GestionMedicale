using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GM.Core.Models;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Gm.UI.Areas.Gestion.Models;
using MvcPaging;

namespace Gm.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceUtilisateur _service;
        private readonly IServicePharmacie _servicePharmacie;

        public HomeController(IServiceUtilisateur service , IServicePharmacie servicePharmacie)
        {
            _service = service;
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

        public ActionResult Pharmaciens(int ? page, string filter)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 1;
            var search = new Pharmacie
            {
                Nom = filter,
                Wilaya = filter,
                Commune = filter,
                Tel = filter,
                Email = filter
            };
            var result = Mapper.Map<IList<PharmacieModel>>(_servicePharmacie.SearchResult(search));
            return View(result.ToPagedList(currentPageIndex, 10));
            
        }
    }
}