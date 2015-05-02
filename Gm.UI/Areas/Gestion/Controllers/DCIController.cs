using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Categorie;
using GM.Services.Nomenclature;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class DciController : Controller
    {
        // GET: Gestion/DCI
        private readonly IServiceDci _serviceDci;
        private readonly IEnumerable<Specialite> _specilites; 
         

        public DciController(IServiceDci serviceDci , IServiceSpecialite serviceSpecialite)
        {
            _serviceDci = serviceDci;
            _specilites = serviceSpecialite.ListeSpecialites();
        }

        // GET: Gestion/Specialite
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListDci( DataSourceRequest request)
        {
            return Json(_serviceDci.ListeDcis().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewData["specialites"] = new SelectList(_specilites, "Id", "Libelle");
            return View();
        }

        public ActionResult Update(int? id)
        {
            if (id == null) return HttpNotFound();
            var item = _serviceDci.FindSingle((int)id);
            if (item == null) return HttpNotFound();
            ViewData["specialites"] = new SelectList(_specilites, "Id", "Libelle", item.SpecialiteId);
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(Dci specialite)
        {
            if (ModelState.IsValid)
            {
                var b = _serviceDci.Update(specialite);
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
        public ActionResult Create(Dci specialite)
        {
            if (ModelState.IsValid)
            {
                var b = _serviceDci.Insert(specialite);
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

            return View(specialite);
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

            var b =id != null&& _serviceDci.Delete((int) id)  ;
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