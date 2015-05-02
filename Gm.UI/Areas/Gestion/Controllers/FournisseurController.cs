using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GM.Core;
using GM.Core.Models;
using GM.Services.Fournisseurs;
using Gm.UI.Areas.Gestion.Models;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class FournisseurController : Controller
    {
        private readonly IServiceFournisseur _service;
        // GET: Gestion/Fournissseur
        public FournisseurController(IServiceFournisseur service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NouvelleEntreprise(Guid id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            if (_service.Existe(id)) return RedirectToAction("Index", "Home", new { area = "" });
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            var model = new PharmacieModel { PropreitaireId = id };
            ViewData["propId"] = id;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "pharmacien")]
        public ActionResult NouvelleEntreprise(PharmacieModel model, HttpPostedFileBase image)
        {

            if (image != null && image.ContentLength > 0)
            {
                var fileName = model.PropreitaireId + Path.GetFileName(image.FileName);
                model.Logo = SaveFile(fileName, image);
            }
            //model.PropreitaireId = Guid.Parse(ViewData["propId"].ToString());
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
            if (_service.Insert(AutoMapper.Mapper.Map<Fournisseur>(model)))
                return RedirectToAction("Info", "Home", new { area = "" });
            return View(model);
        }
        private string SaveFile(string fileName, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/photos/logos"), fileName);
            file.SaveAs(path);
            return path;
        }
    }
}