using System;
using System.Web.Mvc;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class PharmacienController : Controller
    {
        // GET: Gestion/Pharmacien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NouvellePharmacie(Guid? id)
        {
            return View();
        }
    }
}