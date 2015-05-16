using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using GM.Core;
using GM.Core.Models;
using GM.Services.Fournisseurs;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MvcPaging;

namespace Gm.UI.Areas.Gestion.Controllers
{

    public class FournisseurController : Controller
    {
        private readonly IServiceFournisseur _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        // GET: Gestion/Fournissseur
        public FournisseurController(IServiceFournisseur service,
            IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
        }

        [Authorize(Roles = "distributeur")]
        public ActionResult Index()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            var id = _service.GetFournisseur(user.Id);
            if (id == 0) return RedirectToAction("NouvelleEntreprise", "Gestion/Fournisseur", new {id = user.Id});
            if (id != 0 && !user.Validation) return RedirectToAction("Info", "Home", new {area = ""});
            return View();
        }

        [Authorize(Roles = "distributeur, pharmacien")]
        public ActionResult Recherche(int? page, string filter)
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
            var filtredList = _service.FounisseurInscript();
            var result = Mapper.Map<IList<PharmacieModel>>(filtredList);
            return View(result.ToPagedList(currentPageIndex, 8));
        }

        [Authorize(Roles = "distributeur")]
        [HttpGet]
        public ActionResult NouvelleEntreprise(Guid id)
        {
            if (id == Guid.Empty) return HttpNotFound();
            if (_service.Existe(id)) return RedirectToAction("Index", "Home", new {area = ""});
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom");
            var model = new PharmacieModel {PropreitaireId = id};
            ViewData["propId"] = id;
            return View(model);
        }

        [Authorize(Roles = "distributeur")]
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
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom", model.Wilaya);
            if (_service.Insert(Mapper.Map<Fournisseur>(model)))
                return RedirectToAction("Info", "Home", new {area = ""});
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult ListeFournisseur()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "pharmacien")]
        public ActionResult Create(PharmacieModel model)
        {
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom", model.Wilaya);
            if (ModelState.IsValid)
            {
                var b = _service.Insert(Mapper.Map<Fournisseur>(model));
                if (Request.IsAjaxRequest())
                {
                    dynamic data = new
                    {
                        message = b ? SuccessMessage() : ErrorMessage()
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {

            var b = id != null && _service.Delete((int)id);
            var data = new
            {
                message = (b) ? SuccessMessage() : ErrorMessage(),
                //data = _serviceUtilisateur.NonActiveUsers().Count()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(int? id)
        {
            if (id == null) return HttpNotFound();
            var item = _service.SingleFournisseur((int)id);
            if (item == null) return HttpNotFound();
            var model = Mapper.Map<PharmacieModel>(item);
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom", model.Wilaya);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(PharmacieModel fournisseur)
        {
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "Nom", "Nom", fournisseur.Wilaya);
           
                var model = Mapper.Map<Fournisseur>(fournisseur);
                var b = _service.Update(model);

                if (Request.IsAjaxRequest())
                {
                    dynamic data = new
                    {
                        message = b ? SuccessMessage() : ErrorMessage()
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
           
            return View(fournisseur);
        }
        [Authorize(Roles = "pharmacien")]
        public ActionResult GetListFournisseur(DataSourceRequest request)
        {
            var result = Mapper.Map<IList<PharmacieModel>>(_service.FounisseurNonInscript());
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private string SaveFile(string fileName, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/photos/logos"), fileName);
            file.SaveAs(path);
            return path;
        }
        private string SuccessMessage()
        {
            return "<div class='alert alert-info'><p>l'operation est terminée avec succés!</p><div/>";
        }
        private string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }
    }
}