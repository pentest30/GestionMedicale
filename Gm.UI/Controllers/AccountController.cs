using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using Gm.UI.Models;
using GM.Core;
using GM.Core.Models;

namespace Gm.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceUtilisateur _service;
        private readonly IEnumerable<Role> _roles;
        // GET: Account
        public AccountController(IServiceUtilisateur service)
        {
            _service = service;
            _roles = _service.SelectRoles();
        }
        
     
        [HttpGet]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Inscription()
        {
            if (Request.IsAuthenticated) return RedirectToAction("Index", "Home");
            var genres = GetGenre();
            ViewData["Sexe"] = new SelectList(genres, "Key","Value");
            ViewData["RoleId"] = new SelectList(_service.SelectRoles(), "Id", "Nom");
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            return View(new RegisterModel());
        }

        
        private static IEnumerable<KeyValuePair<string, string>> GetGenre()
        {
            IDictionary<string, string> genres = new Dictionary<string, string>();
            genres.Add("Homme", "Homme");
            genres.Add("Femme", "Femme");
            return genres;
        }


        [AllowAnonymous]
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Inscription(RegisterModel model)
        {
            if (Request.IsAuthenticated) return RedirectToAction("Index", "Home");
            InitDropDownList(model);
            if (ModelState.IsValid)
            {
                if (IsNumeric(model.DateOfBirthDay.ToString()) && IsNumeric(model.DateOfBirthYear.ToString()) &&
                    IsNumeric(model.DateOfBirthMonth.ToString()))
                {
                    model.DateNaissance =
                        Convert.ToDateTime(string.Format("{0}/{1}/{2}", model.DateOfBirthDay, model.DateOfBirthMonth,
                            model.DateOfBirthYear));
                }
                if (_service.ExisteDeja(model.Email))
                {
                    ModelState.AddModelError("EmailAddresse", "Email En utilisation, choiser un autre Email");
                    if (_service.ExisteDeja(model.Pseudo)) ModelState.AddModelError("Identifiant", "Identifiant En utilisation, choiser un autre Identifiant");
                   return View(model);
                }
                var roleIds = new int?[1];
                roleIds[0] = model.RoleId;
                var user = Mapper.Map<Utilisateur>(model);
                user.Id = Guid.NewGuid();
                user.DateInscription = DateTime.Now;
                if (_service.Inscription(user, model.Password, roleIds))
                {
                    var role = _roles.SingleOrDefault(x => x.Id == model.RoleId);
                    if (role != null)
                    {
                        var r = role.Nom;
                        switch (r)
                        {
                            case "patient":
                                break;
                            case "medecin":
                                break;
                            case "pharmacien":
                            {
                                return RedirectToAction("NouvellePharmacie", "Gestion/Pharmacien", new {id = user.Id});
                            }
                        }
                    }
                }
            }
          return View(model);
        }

        public ActionResult Logout()
        {
            _service.Logout();
            return RedirectToAction("Index", "Home");
        }
        private void InitDropDownList(RegisterModel model)
        {
            ViewData["RoleId"] = new SelectList(_service.SelectRoles(), "Id", "Nom", model.RoleId);
            ViewData["Sexe"] = new SelectList(GetGenre(), "Key", "Value", model.Sexe);
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}