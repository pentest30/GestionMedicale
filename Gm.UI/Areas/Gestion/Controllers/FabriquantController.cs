﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Fabriquant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "pharmacien,distributeur")]
    public class FabriquantController : Controller
    {
        private readonly IServiceFabriquant _service;
        private readonly IEnumerable<Pays> _pays; 
        public FabriquantController(IServiceFabriquant service)
        {
            _service = service;
            _pays = _service.ListePays();
        }

        // GET: Gestion/Fabriquant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList(DataSourceRequest request)
        {
           return Json(_service.Liste().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewData["pays"] = new SelectList(_pays, "Nom", "Nom");
            return View();
        }

        public ActionResult Update(int? id)
        {
            if (id == null) return HttpNotFound();
            var item = _service.FindSingle((int)id);
            if (item == null) return HttpNotFound();
            ViewData["pays"] = new SelectList(_pays, "Nom", "Nom", item.Pays);
           
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(Laboratoire laboratoire)
        {
            ViewData["pays"] = new SelectList(_pays, "Nom", "Nom", laboratoire.Pays);
            if (ModelState.IsValid)
            {
                var b = _service.Update(laboratoire);
                if (Request.IsAjaxRequest())
                {
                    dynamic data = new
                    {
                        message = b ? SuccessMessage() : ErrorMessage()
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Laboratoire laboratoire)
        {
            ViewData["pays"] = new SelectList(_pays, "Nom", "Nom", laboratoire.Pays);
            if (ModelState.IsValid)
            {
                var b = _service.Insert(laboratoire);
                if (Request.IsAjaxRequest())
                {
                    dynamic data = new
                    {
                        message = b ? SuccessMessage() : ErrorMessage()
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else if (Request.IsAjaxRequest())
            {
                var data = Validate();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return View(laboratoire);
        }

        private string Validate()
        {
            var data = ModelState.Values.SelectMany(val => val.Errors)
                .Aggregate("<div class='alert alert-danger'>",
                    (current, error) => current + "<p>" + error.ErrorMessage + "<p/>");
            data = data + "</div>";
            return data;
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