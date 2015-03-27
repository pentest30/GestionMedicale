using System;
using System.IO;
using System.Web;
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
            ViewData["RoleId"] = new SelectList(_service.SelectRoles(), "Id", "Nom");
            return View(new RegisterModel());
        }

        public ActionResult RetrunInscription()
        {
            ViewBag.RoleId = new SelectList(_service.SelectRoles(), "Id", "Nom");
            return View(new  RegisterModel ());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Inscription(RegisterModel model, HttpPostedFileBase photoPersonnel, HttpPostedFileBase photoPiece)
        {
            if (ModelState.IsValid)
            {
               
                int?[] roleIds = new int?[0];
                roleIds[0] = model.RoleId;
                var user = Mapper.Map<Utilisateur>(model);
                var lienPhotoPersonel = user.Id + Path.GetFileName(photoPersonnel.FileName);
                var lienPhotoPiece = user.Id + Path.GetFileName(photoPiece.FileName);
                user.Id= Guid.NewGuid();
                user.LienPhotoPersonnelle = "~/App_Data/photos/"  +lienPhotoPersonel;
                user.LienPhotoDocument = "~/App_Data/photos/" + lienPhotoPiece;
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), lienPhotoPersonel);
                photoPersonnel.SaveAs(path);
                path = Path.Combine(Server.MapPath("~/App_Data/uploads"), lienPhotoPiece);
                photoPersonnel.SaveAs(path);
                _service.Inscription(user, model.Password,roleIds);

            }
            ViewBag.RoleId = new SelectList(_service.SelectRoles(), "Id", "Nom" , model.RoleId);
            return View(model);
        }
    }
}