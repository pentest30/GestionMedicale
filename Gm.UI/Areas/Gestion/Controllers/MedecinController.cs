using System;
using System.Web.Mvc;
using GM.Core;
using GM.Core.Models;
using GM.Services.Cabinets;
using GM.Services.Utilisateurs;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class MedecinController : Controller
    {
        private readonly IServiceCabinet _serviceCabinet;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        // GET: Gestion/Medecin
        public MedecinController(IServiceCabinet serviceCabinet , IServiceUtilisateur   serviceUtilisateur)
        {
            _serviceCabinet = serviceCabinet;
            _serviceUtilisateur = serviceUtilisateur;
        }

        public ActionResult Index()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            if (user == null || !user.Validation) return RedirectToAction("Info", "Home", new { area = "" });
            return View();
        }

        public ActionResult NouveauCabinet(Guid id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            if (_serviceCabinet.Existe(id)) return RedirectToAction("Index", "Home", new { area = "" });
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            var model = new Cabinet { MedecinId = id };
            ViewData["propId"] = id;
            return View(model);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "pharmacien")]
        public ActionResult NouveauCabinet(Cabinet model)
        {
            //model.PropreitaireId = Guid.Parse(ViewData["propId"].ToString());
            ModelState.Remove("Id");
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
            if (ModelState.IsValid)
            {
                if (_serviceCabinet.Insert(model))
                    return RedirectToAction("Info", "Home", new { area = "" });
            }
           
            return View(model);
        }
    }
}