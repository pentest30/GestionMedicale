using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using GM.Core.Models;
using GM.Services.Entrees;
using GM.Services.Fournisseurs;
using GM.Services.Magasins;
using GM.Services.Medicaments;
using GM.Services.Pharmacies;
using GM.Services.Stocks;
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
        private readonly IServiceMedicmaent _serviceMedicmaent;
        private readonly IServiceEntrees _serviceEntrees;
        private readonly IServiceStock _serviceStock;
        //private readonly IEnumerable<Magasin> _listMagasins;
        private readonly IEnumerable<Fournisseur> _liste;
        // GET: Gestion/BonEntree
        public BonEntreeController(IServiceMagasin serviceMagasin,
            IServiceUtilisateur serviceUtilisateur ,
            IServiceFournisseur serviceFournisseur,
            IServicePharmacie servicePharmacie,
            IServiceMedicmaent serviceMedicmaent,
            IServiceEntrees serviceEntrees , 
            IServiceStock serviceStock)
        {
            _serviceMagasin = serviceMagasin;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
            _servicePharmacie = servicePharmacie;
            _serviceMedicmaent = serviceMedicmaent;
            _serviceEntrees = serviceEntrees;
            _serviceStock = serviceStock;
            _liste = _serviceFournisseur.GeltList();
           // _listMagasins = _serviceMagasin.Liste()
            
        }

        public ActionResult Index()
        {
            Session["entreprise"] = null;
            GetEntrepriseId();
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(Convert.ToInt32(Session["entreprise"])), "Id",
              "Libelle");
            return View();
        }
        public ActionResult GetList(DataSourceRequest request)
        {
            return Json(_serviceEntrees.Liste(Convert.ToInt32(Convert.ToInt32(Session["entreprise"]))).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            GetEntrepriseId();
            ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(Convert.ToInt32(Session["entreprise"])), "Id",
                "Libelle");
            var model = new Entree
            {
                ClientId = Convert.ToInt32(Session["entreprise"])
            };
            return View(model);
        }
        public ActionResult Update(long? id)
        {
            if (id == null) return HttpNotFound();
            var model = _serviceEntrees.FindSingle(Convert.ToInt64(id));
            ViewData["id"] = model.Id;
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            return View(model);
        }
        public ActionResult Create(Entree model, bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom" , model.FournisseurId);
            GetEntrepriseId();
            if (ModelState.IsValid)
            {
                long identity;
                _serviceEntrees.Insert(model, out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id = identity;
            }
            model.ClientId = Convert.ToInt32(Session["entreprise"]);

            return (continuer) ? View(model) : View("Index");
        }
        [HttpPost]
        public ActionResult Update(Entree model, bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            GetEntrepriseId();
            if (ModelState.IsValid)
            {
                //long identity;
                _serviceEntrees.Update(model);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id ;
            }
            model.ClientId = Convert.ToInt32(Session["entreprise"]);

            return (continuer) ? View(model) : View("Index");
        }
        public ActionResult SearchMedicament()
        {
            var name = Request["q"];
            if (string.IsNullOrWhiteSpace(name)) return Json(new List<Medicament>(), JsonRequestBehavior.AllowGet);
            var dci = new Dci
            {
                Nom = name
            };
            var filter = new Medicament
            {
                NomCommerciale = name,
                Code = name,
                NumEnregistrement = name,
                Dci = dci,
                Dose = name
                // LaboratoireId = Convert.ToInt32(labId)
            };
            var list = Mapper.Map<IList<MedicamentModel>>(_serviceMedicmaent.AutoCOmpleteListe(filter));

            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetBonMagasin(long? id)
        {
            if (id == null) return HttpNotFound();
            var facture = _serviceEntrees.FindSingle(Convert.ToInt64(id));
            if (facture != null)
            {
                ViewData["magasin"] = new SelectList(_serviceMagasin.Liste(facture.ClientId), "Id", "Libelle");
                var model = Mapper.Map<BonEntree>(facture);
                model.FactureId = Convert.ToInt64(id);
                return PartialView("_BonMagasin", model);
            }
            return PartialView("_BonMagasin", new BonEntree());
        }

        [HttpPost]
        public ActionResult CreateBonMagasin(BonEntree model)
        {
            var bon = _serviceEntrees.FindByFactureId(model.FactureId);
            var b = false;
            var facture = _serviceEntrees.FindSingle(Convert.ToInt64(model.FactureId));
            if (bon != null && bon.LigneEntreesMagasin.Count() != facture.LigneEntrees.Count())
            {

                foreach (var ligne in facture.LigneEntrees)
                {
                    var findSingle = _serviceStock.FindSingle(ligne.MedicamentId, model.MagasinId);
                    if (findSingle != null)
                    {
                        findSingle.Qnt += ligne.Qnt;
                        _serviceStock.Update(findSingle);
                    }
                    else
                    {
                        var stoc = new Stock
                        {
                            MagasinId = model.MagasinId,
                            EntrepriseId = model.ClientId,
                            MedicamentId = ligne.MedicamentId , 
                            Qnt = ligne.Qnt
                        };
                        _serviceStock.Insert(stoc);
                    }
                    var ligne1 = ligne;
                    var entreeMagasin =
                        bon.LigneEntreesMagasin.FirstOrDefault(
                            x => x.NumLot == ligne1.NumLot && x.MedicamentId == ligne1.MedicamentId);
                    if (entreeMagasin == null)
                    {
                        var newEntree = Mapper.Map<LigneEntreeMagasin>(ligne);
                        newEntree.BonEntreeId = bon.Id;
                        b = _serviceEntrees.InsertLigneMagasin(newEntree);
                    }
                }
                dynamic data = new
                {
                    message = b ? SuccessMessage() : ErrorMessage()
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (bon != null && bon.LigneEntreesMagasin.Count() == facture.LigneEntrees.Count())
            {
                dynamic data = new
                {
                    message = "Bon d'entrée existe dans la base de données"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }


            long identity;
             b = _serviceEntrees.InsertBonMagasin(model, out identity);
             if (facture.LigneEntrees.Count > 0&& b)
             {
                 foreach (LigneEntree ligne in facture.LigneEntrees)
                 {
                     var findSingle = _serviceStock.FindSingle(ligne.MedicamentId, model.MagasinId);
                     if (findSingle != null)
                     {
                         findSingle.Qnt += ligne.Qnt;
                         _serviceStock.Update(findSingle);
                     }
                     else
                     {
                         var stoc = new Stock
                         {
                             MagasinId = model.MagasinId,
                             EntrepriseId = model.ClientId,
                             MedicamentId = ligne.MedicamentId,
                             Qnt = ligne.Qnt
                         };
                         _serviceStock.Insert(stoc);
                     }
                     var newEntree = Mapper.Map<LigneEntreeMagasin>(ligne);
                     newEntree.BonEntreeId = identity;
                     b = _serviceEntrees.InsertLigneMagasin(newEntree);
                 }
                 dynamic data = new
                 {
                     message = b ? SuccessMessage() : ErrorMessage()
                 };

                 return Json(data, JsonRequestBehavior.AllowGet);
             }
             dynamic data1 = new
             {
                 message = b ? SuccessMessage() : ErrorMessage()
             };
             return Json(data1, JsonRequestBehavior.AllowGet);
         }
        public ActionResult DetailCommade(long? id )
        {
            long commandeId = Convert.ToInt64(Request["commandeId"]);
            if (id == null || id == 0)
            {
                return PartialView("_CreateOrUpdateLigne", new LigneEntree
                {
                    EntreeId = Convert.ToInt64(commandeId)
                });
            }
            
            var ligne = _serviceEntrees.GetSingleLigne(Convert.ToInt64(id));
            ViewData["medicament"] = new SelectList(_serviceMedicmaent.ListeMedicaments(), "Id", "NomCommerciale", ligne.MedicamentId);
            return PartialView("_CreateOrUpdateLigne", ligne);
        }
        public ActionResult CreateUpdateLigne(LigneEntree model)
        {
            if (ModelState.IsValid)
            {
                var b = (model.Id == 0) ? _serviceEntrees.InsertLigne(model) : _serviceEntrees.UpdateLigne(model);
                dynamic data = new
                {
                    message = b ? SuccessMessage() : ErrorMessage()
                };
                ViewData["id"] = model.EntreeId;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        private void GetEntrepriseId()
        {
            var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
            if (User.IsInRole("pharmacien"))
            {
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            else if (User.IsInRole("distributeur"))
            {
                Session["entreprise"] = Convert.ToInt32(_serviceFournisseur.GetFournisseur(user.Id));
            }

        }

        public ActionResult Delete(int? id)
        {
            var b = id != null && _serviceEntrees.Delete((int)id);
            var data = new
            {
                message = (b) ? SuccessMessage() : ErrorMessage(),
                //data = _serviceUtilisateur.NonActiveUsers().Count()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListeLigneEntrees(DataSourceRequest request, int? commandeId)
        {
            return Json(_serviceEntrees.GetLigneCommandes(Convert.ToInt32(commandeId)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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