using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GM.Core.Models;
using GM.Services.Entrees;
using GM.Services.Fournisseurs;
using GM.Services.Magasins;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class BonEntreeController : Controller
    {
        private readonly IServiceMagasin _serviceMagasin;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceFournisseur _serviceFournisseur;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceEntrees _serviceEntrees;
        //private readonly IEnumerable<Magasin> _listMagasins;
        private readonly IEnumerable<Fournisseur> _liste;
        // GET: Gestion/BonEntree
        public BonEntreeController(IServiceMagasin serviceMagasin,
            IServiceUtilisateur serviceUtilisateur ,
            IServiceFournisseur serviceFournisseur,
            IServicePharmacie servicePharmacie,
            IServiceEntrees serviceEntrees)
        {
            _serviceMagasin = serviceMagasin;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
            _servicePharmacie = servicePharmacie;
            _serviceEntrees = serviceEntrees;
            _liste = _serviceFournisseur.GeltList();
            
        }

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
            ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(Convert.ToInt32(Session["entreprise"])), "Id",
                "Libelle");
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            return View();
        }
        public ActionResult GetList(DataSourceRequest request)
        {
            return Json(_serviceEntrees.Liste(Convert.ToInt32(Convert.ToInt32(Session["entreprise"]))).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}