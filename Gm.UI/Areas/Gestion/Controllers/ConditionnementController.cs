﻿using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Conditionnelts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
     [Authorize(Roles = "pharmacien,distributeur")]
    public class ConditionnementController : Controller
    {
        // GET: Gestion/Conditionnemet
        private readonly IServiceConditionnement _service;
        // GET: Gestion/Conditionnement
        public ConditionnementController(IServiceConditionnement service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllConditionnements( DataSourceRequest request)
        {
            return Json(_service.Liste().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update(int? id)
        {
            if (id == null) return HttpNotFound();
            var item = _service.FindSingle((int)id);
            if (item == null) return HttpNotFound();
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(Conditionnement conditionnement)
        {
            if (ModelState.IsValid)
            {
                var b = _service.Update(conditionnement);
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
        public ActionResult Create(Conditionnement conditionnement)
        {

            if (ModelState.IsValid)
            {
                var b = _service.Insert(conditionnement);
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

            return View(conditionnement);
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