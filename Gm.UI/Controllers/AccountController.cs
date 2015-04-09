using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using GM.Core;
using GM.Core.Models;
using Gm.UI.Models.Utilisateurs;

namespace Gm.UI.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        private readonly IServiceUtilisateur _service;
        private readonly IServicePharmacie _servicePharmacie;
        private readonly IEnumerable<Role> _roles;
        // GET: Account
        public AccountController(IServiceUtilisateur service, IServicePharmacie servicePharmacie)
        {
            _service = service;
            _servicePharmacie = servicePharmacie;
            _roles = _service.SelectRoles();
        }
        [HttpGet]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Inscription()
        {
            if (Request.IsAuthenticated) return RedirectToAction("Index", "Home");
            ViewData["Sexe"] = new SelectList(GetGenre(), "Key", "Value");
            ViewData["RoleId"] = new SelectList(_roles, "Id", "Nom");
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
                        var r = role.Nom.ToLower();
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
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home", new { area = "" });
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home", new { area = "" });
            if (ModelState.IsValid)
            {
                var item = _service.SingleUser(model.Identifiant);
                if (item != null)
                {
                    var b = _service.Authentification(item, model.Password, model.RememberMe);
                    var role = _service.Role(item);
                    if (item.Validation && b)
                    {
                       if (!string.IsNullOrEmpty(role) &&(role.Equals("pharmacien") || role.Equals("pharmacien-vendeur")))
                        {
                            //var pharmacieId = _servicePharmacie.GetPharmacie(item.Id);
                            if (role.Equals("pharmacien"))
                            {
                                var pharmacieId = _servicePharmacie.GetPharmacie(item.Id);
                                return RedirectToAction("Index", "Gestion/Pharmacien", new {id = pharmacieId});
                            }
                            return ((item.EnrepriseId != null))
                                ? RedirectToAction("Index", "Gestion/Pharmacien", new {id = item.EnrepriseId})
                                : RedirectToAction("Index", "Home", new { area = "" });
                        }
                        if (User.IsInRole("distributeur") || User.IsInRole("distributeur-vendeur"))
                        {

                        }
                        else if (User.IsInRole("medecin"))
                        {

                        }
                        else if (User.IsInRole("patient"))
                        {

                        }
                    }
                    else if (b)
                    {
                        if (role == "pharmacien")
                        {
                            var pharmacieId = _servicePharmacie.GetPharmacie(item.Id);
                            return (pharmacieId == 0)
                                ? RedirectToAction("NouvellePharmacie", "Gestion/Pharmacien", new {id = item.Id})
                                : RedirectToAction("Info", "Home", new {area = ""});

                        }

                    }
                    else 
                    {
                        ViewData["info"] = "Password est faux essayez vous une autre fois.";
                    }
                }
                else
                {
                    ViewData["info"] = "Ce compte n'existe pas.";
                }
               
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _service.Logout();
            return RedirectToAction("Index", "Home" , new {area=""});
        }
        private void InitDropDownList(RegisterModel model)
        {
            ViewData["RoleId"] = new SelectList(_roles, "Id", "Nom", model.RoleId);
            ViewData["Sexe"] = new SelectList(GetGenre(), "Key", "Value", model.Sexe);
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}