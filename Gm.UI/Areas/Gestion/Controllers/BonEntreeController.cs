﻿using System;
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
        //private readonly IEnumerable<Magasin> _listMagasins;
        private readonly IEnumerable<Fournisseur> _liste;
        // GET: Gestion/BonEntree
        public BonEntreeController(IServiceMagasin serviceMagasin,
            IServiceUtilisateur serviceUtilisateur ,
            IServiceFournisseur serviceFournisseur,
            IServicePharmacie servicePharmacie,
            IServiceMedicmaent serviceMedicmaent,
            IServiceEntrees serviceEntrees)
        {
            _serviceMagasin = serviceMagasin;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceFournisseur = serviceFournisseur;
            _servicePharmacie = servicePharmacie;
            _serviceMedicmaent = serviceMedicmaent;
            _serviceEntrees = serviceEntrees;
            _liste = _serviceFournisseur.GeltList();
            
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
        public ActionResult DetailCommade(long? id )
        {
            long commandeId = Convert.ToInt64(Request["commandeId"]);
            if (id == null || id == 0)
            {
                return PartialView("_CreateOrUpdateLigne", new LigneEntree()
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