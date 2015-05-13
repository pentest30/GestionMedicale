using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Core;
using GM.Core.Models;
using GM.Services.Fournisseurs;
using Gm.UI.Areas.Gestion.Models;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MvcPaging;

namespace Gm.UI.Areas.Gestion.Controllers
{
     [Authorize(Roles = "distributeur")]
    public class FournisseurController : Controller
    {
        private readonly IServiceFournisseur _service;
         private readonly IServiceUtilisateur _serviceUtilisateur;
         // GET: Gestion/Fournissseur
        public FournisseurController(IServiceFournisseur service,IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
        }

         public ActionResult Index()
         {
             var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
             var id = _service.GetFournisseur(user.Id);
             if (id == 0)  return RedirectToAction("NouvelleEntreprise", "Gestion/Fournisseur", new { id = user.Id });
             if (id !=0 &&! user.Validation)return  RedirectToAction("Info", "Home", new {area = ""});
             return View();
         }

         public ActionResult Recherche(int? page , string filter)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 1;
            var search = new Fournisseur
            {
                Nom = filter,
                Wilaya = filter,
                Commune = filter,
                Tel = filter,
                Email = filter
            };
            var result = Mapper.Map<IList<PharmacieModel>>(_service.SearchResult(search));
            return View(result.ToPagedList(currentPageIndex,  10));
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
            if (_service.Insert(Mapper.Map<Fournisseur>(model)))
                return RedirectToAction("Info", "Home", new { area = "" });
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult ListeFournisseur()
        {
            return View();
        }
       
        [Authorize(Roles = "pharmacien")]
        public ActionResult GetListFournisseur(DataSourceRequest request )
        {
            var result = Mapper.Map<IList<PharmacieModel>>(_service.GeltList());
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private string SaveFile(string fileName, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/photos/logos"), fileName);
            file.SaveAs(path);
            return path;
        }
    }
}