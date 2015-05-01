using System;
using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Magasins;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "pharmacien")]
    public class MagasinController : Controller
    {
        private readonly IServiceMagasin _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;

        public MagasinController(
            IServiceMagasin service, 
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            
        }

        // GET: Gestion/Magasin
        public ActionResult Index()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            int? id = _servicePharmacie.GetPharmacie(user.Id);
            Session["entreprise"] = id; 
            return View();
        }

        public ActionResult GetList([DataSourceRequest] DataSourceRequest request)
        {
            int? id;
            if (Session["entreprise"] != null)
               id = Convert.ToInt32(Session["entreprise"]);
            else
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                id = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
          
            return Json(_service.Liste(Convert.ToInt32(id)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id == null) return HttpNotFound();
            var item = _service.FindSingle((int)id);
            if (item == null) return HttpNotFound();
            
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(Magasin laboratoire)
        {
         
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
        public ActionResult Create(Magasin laboratoire)
        {
            if (Session["entreprise"] != null)
                laboratoire.EntrepriseId = Convert.ToInt32(Session["entreprise"]);
            else
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                laboratoire.EntrepriseId = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
          
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