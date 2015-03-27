using System;
using System.Collections.Generic;
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
        // GET: Account
        public AccountController(IServiceUtilisateur service)
        {
            _service = service;
        }
        
     
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Inscription()
        {
            if (Request.IsAuthenticated) return RedirectToAction("Index", "Home");
            var genres = GetGenre();
            ViewData["Sexe"] = new SelectList(genres, "Key","Value");
            ViewData["RoleId"] = new SelectList(_service.SelectRoles(), "Id", "Nom");
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom");
            return View(new RegisterModel());
        }

        private static IEnumerable<KeyValuePair<int, string>> GetGenre()
        {
            IDictionary<int, string> genres = new Dictionary<int, string>();
            genres.Add(1, "Homme");
            genres.Add(2, "Femme");
            return genres;
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription(RegisterModel model)
        {
           
            if (IsNumeric(model.DateOfBirthDay.ToString()) && IsNumeric(model.DateOfBirthYear.ToString()) &&
                IsNumeric(model.DateOfBirthMonth.ToString()))
            {
                model.DateNaissance = Convert.ToDateTime(string.Format("{0}/{1}/{2}", model.DateOfBirthDay, model.DateOfBirthMonth,
                model.DateOfBirthYear));
            }
            if (ModelState.IsValid)
            {
                if (_service.ExisteDeja(model.Email))
                {
                    ModelState.AddModelError("EmailAddresse", "Email En utilisation, choiser un autre Email");
                    return View(model);
                }
                if (_service.ExisteDeja(model.Pseudo))
                {
                    ModelState.AddModelError("Identiffiant", "Identifiant En utilisation, choiser un autre Identifiant");
                    return View(model);
                }
                var roleIds = new int?[0];
                roleIds[0] = model.RoleId;
                var user = Mapper.Map<Utilisateur>(model);
                user.Id = Guid.NewGuid();
                if (_service.Inscription(user, model.Password, roleIds))
                {
                    
                }
            }
            ViewData["RoleId"] = new SelectList(_service.SelectRoles(), "Id", "Nom", model.RoleId);
            ViewData["Sexe"] = new SelectList(GetGenre(), "Key", "Value", model.Sexe);
            ViewData["Wilaya"] = new SelectList(Wilaya.ListWilayas(), "NumWilaya", "Nom", model.Wilaya);
            return View(model);
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}