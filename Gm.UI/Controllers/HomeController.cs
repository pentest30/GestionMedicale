using System.Web.Mvc;
using GM.Services.Utilisateurs;

namespace Gm.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceUtilisateur _service;

        public HomeController(IServiceUtilisateur service)
        {
            _service = service;
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //ViewData["Roles"] = _service.SelectRoles();
            return View();
        }
    }
}