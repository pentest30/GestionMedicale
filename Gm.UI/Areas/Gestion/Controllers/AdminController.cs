using System.Web.Mvc;
using GM.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceAdministrateur _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;


        public AdminController(IServiceAdministrateur service, IServiceUtilisateur serviceUtilisateur )
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
        }

        // GET: Gestion/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Utilisateurs()
        {
            return View();
        }

        public ActionResult AllUsers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }
    }
}