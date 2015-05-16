using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Formes;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
     [Authorize(Roles = "pharmacien,distributeur")]
    public class FormeController : Controller
    {
        private readonly IServiceForme _service;
        // GET: Gestion/Forme
        public FormeController(IServiceForme service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllFormes(DataSourceRequest request)
        {
            return Json(_service.ListeFormes().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(Forme frome)
        {
            if (ModelState.IsValid)
            {
                var b = _service.Update(frome);
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
        public ActionResult Create(Forme forme)
        {

            if (ModelState.IsValid)
            {
                var b = _service.Insert(forme);
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

            return View(forme);
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