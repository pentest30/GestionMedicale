using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Gm.UI.Areas.Gestion.Models;
using GM.Core;

namespace Gm.UI.Areas.Gestion.Controllers
{
  
    public class PharmacienController : Controller
    {
        private readonly IServicePharmacie _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        // GET: Gestion/Pharmacien
        //[Authorize(Roles = "pharmacien, pharmacien-vendeur")]
        public PharmacienController(IServicePharmacie service , IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
        }
        [Authorize(Roles = "pharmacien")]
        public ActionResult Index()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            if (user== null ||! user.Validation)return  RedirectToAction("Info", "Home", new {area = ""});
            return View();
        }
        [HttpGet]
        public ActionResult Compte()
        {
             var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            var id = _service.GetPharmacie(user.Id);
            var pharmacie = _service.SinglePharmacie(Convert.ToInt32(id));
            return View(pharmacie);
        }

        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult EditPharmacie()
        {

            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            var id = _service.GetPharmacie(user.Id);
            var pharmacie =AutoMapper.Mapper.Map<PharmacieModel>( _service.SinglePharmacie(Convert.ToInt32(id)));
            pharmacie.Logo = string.IsNullOrWhiteSpace(pharmacie.Logo)
                ? Path.Combine(Server.MapPath("~/App_Data/photos/logos"), "emptyMME.gif")
                : pharmacie.Logo;
            ViewData["logo"] = pharmacie.Logo;
            return View(pharmacie);
        }
        [Authorize(Roles = "pharmacien")]
        public ActionResult SomeImage(string imageName)
        {
            var path = string.IsNullOrWhiteSpace(imageName)
                ? Path.Combine(Server.MapPath("~/App_Data/photos/logos"), "emptyMME.gif")
                : imageName;
            path = Path.GetFullPath(path);
            return File(path, "image/jpeg");
        }

        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult NouvellePharmacie(Guid? id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            if (id != null && _service.Existe(id.Value)) return RedirectToAction("Index", "Home", new { area = "" });
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            var model = new PharmacieModel {PropreitaireId = id};
            ViewData["propId"] = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "pharmacien")]
        public ActionResult NouvellePharmacie(PharmacieModel model, HttpPostedFileBase file)
        {
            
            if (file != null && file.ContentLength > 0)
            {
                var fileName = model.PropreitaireId + Path.GetFileName(file.FileName);
                model.Logo = SaveFile(fileName , file);
            }
            //model.PropreitaireId = Guid.Parse(ViewData["propId"].ToString());
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
            if (_service.Insert(AutoMapper.Mapper.Map<Pharmacie>(model)))
                return RedirectToAction("Info", "Home", new {area = ""});
            return View(model);
        }
        private string SaveFile(string fileName, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/photos/logos"), fileName);
            file.SaveAs(path);
            return path;
        }
        [HttpPost]
        [Authorize(Roles = "pharmacien")]
        public ActionResult UpdateLogo( int? id)
        {
            var files =   Request.Files[0];
            var phar = _service.SinglePharmacie(Convert.ToInt32(id));
            if (files != null)
            {
                var paht= SaveFile(files.FileName, files);
                phar.LogoUrl = paht;
                _service.Update(phar);
                ViewData["logo"] = phar.LogoUrl;
                return Json(paht, JsonRequestBehavior.AllowGet);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}