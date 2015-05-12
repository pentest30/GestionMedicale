using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gm.UI.Areas.Gestion.Models;
using GM.Services.Fournisseurs;
using GM.Services.Pharmacies;
using GM.Services.Stocks;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class StockController : Controller
    {
        private readonly IServiceStock _serviceStock;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceFournisseur _serviceFournisseur;
        // GET: Gestion/Stock
        public StockController(IServiceStock serviceStock,
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur , 
            IServiceFournisseur serviceFournisseur)
        {
            _serviceStock = serviceStock;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
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
            return View();
        }

        public ActionResult ListStock(DataSourceRequest request)
        {
            var result =
                AutoMapper.Mapper.Map<IList<StockModel>>(_serviceStock.Liste(Convert.ToInt32(Session["entreprise"])));
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}