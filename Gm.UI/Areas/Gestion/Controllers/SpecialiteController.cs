using System.Web.Mvc;
using GM.Core;
using GM.Core.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class SpecialiteController : Controller
    {
        private readonly IServiceSpecialite _service;

        public SpecialiteController(IServiceSpecialite service)
        {
            _service = service;
        }

        // GET: Gestion/Specialite
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListSpecialite([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.ListeSpecialites().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                var b = _service.Update(specialite);
                if (Request.IsAjaxRequest())
                {
                    dynamic data = new
                    {
                        message =b ? SuccessMessage() : ErrorMessage()
                    };
                   
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                var b = _service.Insert(specialite);
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

        public ActionResult Delete(int? id)
        {

            var b =id != null&& _service.Delete((int) id)  ;
            var data = new
                {
                    message =(b)?SuccessMessage(): ErrorMessage(),
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