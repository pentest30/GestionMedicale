using System;
using System.Linq;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Fournisseurs;
using GM.Services.Magasins;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "pharmacien, distributeur")]
    public class MagasinController : Controller
    {
        private readonly IServiceMagasin _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceFournisseur _serviceFournisseur;

        public MagasinController(
            IServiceMagasin service, 
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur , IServiceFournisseur serviceFournisseur)
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
        }

        // GET: Gestion/Magasin
        public ActionResult Index()
        {
            Session["entreprise"] = null;
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            if (User.IsInRole("pharmacien"))
            {
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            else if (User.IsInRole("distributeur"))
            {
                Session["entreprise"] = Convert.ToInt32(_serviceFournisseur.GetFournisseur(user.Id));
            }

            return View();
        }

        public ActionResult GetList( DataSourceRequest request)
        {
            return Json(_service.Liste(Convert.ToInt32(Convert.ToInt32(Session["entreprise"]))).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                laboratoire.EntrepriseId = (User.IsInRole("pharmacie"))?
                    Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id)) : 
                    Convert.ToInt32(_serviceFournisseur.GetFournisseur(user.Id));
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