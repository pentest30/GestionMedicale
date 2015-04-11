using System;
using System.Web.Mvc;
using Gm.UI.Areas.Gestion.Models;
using GM.Core;

namespace Gm.UI.Areas.Gestion.Controllers
{
  
    public class PharmacienController : Controller
    {
        // GET: Gestion/Pharmacien
        //[Authorize(Roles = "pharmacien, pharmacien-vendeur")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compte()
        {
            return View();
        }

      
        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult NouvellePharmacie(Guid? id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            var model = new PharmacieModel {PropreitaireId = id};
            return View(model);
        }
    }
}