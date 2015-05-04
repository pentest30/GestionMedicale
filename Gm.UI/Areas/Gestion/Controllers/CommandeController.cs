using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using GM.Core.Models;
using GM.Services.Commandes;
using GM.Services.Fournisseurs;
using GM.Services.Medicaments;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    [Authorize(Roles = "pharmacien,distributeur")]
    public class CommandeController : Controller
    {
        private readonly IServiceCommandes _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IServiceMedicmaent _serviceMedicmaent;
        private readonly IServiceFournisseur _serviceFournisseur;
        private readonly IEnumerable<Fournisseur> _liste;
        private readonly IEnumerable<Pharmacie> _listeClients; 

        public CommandeController(IServiceCommandes service, 
            IServicePharmacie servicePharmacie,
            IServiceUtilisateur serviceUtilisateur, 
            IServiceFournisseur serviceFournisseur , 
            IServiceMedicmaent serviceMedicmaent )
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _serviceUtilisateur = serviceUtilisateur;
            _serviceMedicmaent = serviceMedicmaent;
            _serviceFournisseur = serviceFournisseur;
            _liste = _serviceFournisseur.GeltList();
            _listeClients = _servicePharmacie.GetListe();
        }

        // GET: Gestion/Commande
        public ActionResult Index()
        {
            GetEntrepriseId();
            ViewData["pharmacien"] = new SelectList(_listeClients, "Id", "Nom");
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
           return View();
        }

     

        public ActionResult GetList( DataSourceRequest request)
        {
            return Json(_service.Liste(Convert.ToInt32(Convert.ToInt32(Session["entreprise"]))).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize(Roles = "pharmacien")]
        public ActionResult Create()
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom");
            if (Session["entreprise"] == null)
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            var model = new Commande();
            if (User.IsInRole("pharmacien")) model.ClientId = Convert.ToInt32(Session["entreprise"]);
            else
            {
                model.FournisseurId = Convert.ToInt32(Session["entreprise"]);
            }
            return View(model);
        }
         [Authorize(Roles = "pharmacien")]
        public ActionResult Update(long ? id)
        {
            if (id == null) return HttpNotFound();
            var model = _service.FindSingle(Convert.ToInt64(id));
            ViewData["id"] = model.Id;
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom", model.FournisseurId);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "pharmacien")]
        public ActionResult Create(Commande model , bool continuer)
        {
            ViewData["fournisseur"] = new SelectList(_liste, "Id", "Nom" , model.FournisseurId);
            if (ModelState.IsValid)
            {
                long identity;
                _service.Insert(model , out identity);
                ViewData["info"] = "Opération est terminé avec succéss !";
                ViewData["id"] = model.Id = identity;
            }
            if (Session["entreprise"] == null)
            {
                var user = _serviceUtilisateur.SingleUser(User.Identity.Name);
                Session["entreprise"] = Convert.ToInt32(_servicePharmacie.GetPharmacie(user.Id));
            }
            if (User.IsInRole("pharmacien")) model.ClientId = Convert.ToInt32(Session["entreprise"]);
            return (continuer) ? View(model) : View("Index");
        }
        [HttpPost]
        [Authorize(Roles = "pharmacien")]
        public ActionResult CreateUpdateLigne(LigneCommande model)
        {
            if (ModelState.IsValid)
            {
                var b = (model.Id == 0) ? _service.InsertLigne(model) : _service.UpdateLigne(model);
                dynamic data = new
                {
                    message = b ? SuccessMessage() : ErrorMessage()
                };
                ViewData["id"] = model.CommandeId;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult DetailCommade(long? id, long? commandeId)
        {
            if (id == null || id == 0)
                return PartialView("_CreateOrUpdateLigne", new LigneCommande {CommandeId = Convert.ToInt64(commandeId)});
            var ligne = _service.GetSingleLigne(Convert.ToInt64(id));
            return PartialView("_CreateOrUpdateLigne", ligne);
        }

        [HttpPost]
        [Authorize(Roles = "pharmacien")]
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

        public ActionResult DetailLigne(long? id)
        {
            var com = _service.FindSingle(Convert.ToInt64(id));
            ViewData["client"] = Mapper.Map<PharmacieModel>(_servicePharmacie.SinglePharmacie(com.ClientId));
            var commandes = Mapper.Map<IList<LigneComamndeModel>>( _service.GetLigneCommandes(Convert.ToInt32(id)));
            foreach (var cmd in commandes)
            {
                cmd.Medicament = Mapper.Map <MedicamentModel>(_serviceMedicmaent.ListeMedicaments().FirstOrDefault(x => x.Id == cmd.MedicamentId));
            }
            return PartialView("_DetailsLignes", commandes.ToList());
        }

        public ActionResult ListeLigneCommandes(DataSourceRequest request,int? commandeId)
        {
            return Json(_service.GetLigneCommandes(Convert.ToInt32(commandeId)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private string SuccessMessage()
        {
            return "<div class='alert alert-info'><p>l'operation est terminée avec succés!</p><div/>";
        }
        private string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }
        private void GetEntrepriseId()
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
        }

    }
}