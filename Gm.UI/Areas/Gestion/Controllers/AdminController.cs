using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GM.Core;
using GM.Core.Models;
using Gm.UI.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Gm.UI.Areas.Gestion.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceAdministrateur _service;
        private readonly IServiceUtilisateur _serviceUtilisateur;
        private readonly IDictionary<int, string> _filtreUsers;
        private readonly IList<Role> _roles;

        public AdminController(IServiceAdministrateur service, IServiceUtilisateur serviceUtilisateur)
        {
            _service = service;
            _serviceUtilisateur = serviceUtilisateur;
            _roles = new List<Role>();
            _roles.Add(new Role
            {
                Id = 0,
                Nom = ""
            });
            _roles.AddRange(_serviceUtilisateur.SelectRoles());
            _filtreUsers = new Dictionary<int, string>
            {
                {1, "Tous les utilisateurs"},
                {2, "Comptes actifs"},
                {3, "comptes non actifs"}
            };
        }

        // GET: Gestion/Admin
        public ActionResult Index()
        {
            Session["NewUsers"] = _serviceUtilisateur.NonActiveUsers().Count();
            return View();

        }
        [HttpPost]
        //[ChildActionOnly]
        public ActionResult UserDetails(string id)
        {
            var identity = new Guid(id.Trim());
            var user =_serviceUtilisateur.VoirProfile(identity);
            var model = AutoMapper.Mapper.Map<RegisterModel>(user);
            return View(model);
        }

        public ActionResult Utilisateurs()
        {
            Session["filter"] = 0;
            ViewData["filterUsers"] = new SelectList(_filtreUsers, "Key", "Value");
            ViewData["roles"] = new SelectList(_roles, "Id", "Nom");
            return View();
        }

        [HttpPost]
        //[filte(false)]
        public ActionResult Delete(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.SupprimeCompte(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.AccepteInscription(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());
        }

        [HttpPost]
        public ActionResult Desactive(string id)
        {
            Guid? identity = new Guid(id.Trim());
            if (string.IsNullOrEmpty(id) || !_service.DesactiveCompte(identity))
                return Content(ErrorMessage());
            return Content(SuccessMessage());

        }

        [HttpPost]
        public ActionResult UpdateFilter(int? f)
        {
            Session["filter"] = Convert.ToInt32(f);
            return null;
        }

        public ActionResult AllUsers([DataSourceRequest] DataSourceRequest request, string pseudo, string email)
        {
           
            if (!string.IsNullOrEmpty(pseudo)|| !string.IsNullOrEmpty(email))
            {
                var model = new RegisterModel();
                 model.Pseudo =pseudo;
                model.Email = email;
                var result = Utilisateurs(model) as IList<Utilisateur>;
                return Json(result.ToDataSourceResult(request));
            }
            var filter = Session["filter"].ToString();

            switch (filter)
            {

                case "2":
                    {
                        return Json(_serviceUtilisateur.ActiveUsers().ToDataSourceResult(request));
                    }
                case "3":
                    {
                        return Json(_serviceUtilisateur.NonActiveUsers().ToDataSourceResult(request));
                    }

            }
            return Json(_serviceUtilisateur.AllUsers().ToDataSourceResult(request));
        }

        private string ErrorMessage()
        {
            return "<div class='alert alert-danger'><p>erreurs pendant l'operation!</p><div/>";
        }

        private string SuccessMessage()
        {
            return "<div class='alert alert-success'><p>Suppression est terminer avec succés!</p><div/>";
        }
        private IEnumerable<Utilisateur> Utilisateurs(RegisterModel model)
        {
            var result = new List<Utilisateur>();
            if (!string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Pseudo))
                result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Email.Equals(model.Email)));
            else if (string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Pseudo))
                result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Pseudo == model.Pseudo));
            else if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Pseudo)) result.AddRange(_serviceUtilisateur.AllUsers().Where(x => x.Pseudo == model.Pseudo && x.Email == model.Email));
            return result;
        }

    }
}