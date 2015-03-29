using System;
using System.Web.Mvc;
using Gm.UI.Areas.Gestion.Models;
using GM.Core;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "Pharmacien")]
    public class PharmacienController : Controller
    {
        // GET: Gestion/Pharmacien
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NouvellePharmacie(Guid? id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            var model = new PharmacieModel {PropreitaireId = id};
            return View(model);
        }
    }
}