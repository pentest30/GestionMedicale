using System.Web.Mvc;
using AutoMapper;
using GM.Core.Models;
using GM.Presentation.Models;
using GM.Services.UserServices;

namespace GM.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ServiceUtilisateur _service;
      //  private readonly IList<SelectListItem> _roleList; 

        public AccountController(ServiceUtilisateur service)
        {
            _service = service;
           // _roleList = roleList;
         
            
        }

        // GET: Account
        [HttpGet]
        public ActionResult Inscription()
        {
            var model = new SimpleUserVm();
            ViewBag.RoleId = new SelectList(_service.SelectRoles(), "Id", "Nom");
            return View(model);
        }
        [HttpPost]
        public ActionResult Inscription(SimpleUserVm model)
        {
            var item = Mapper.Map<Utilisateur>(model);
            ViewBag.RoleId = new SelectList(_service.SelectRoles(), "Id", "Nom", model.RoleId);
           // _service.Inscription(item , model.Password);
            return View();
        }
    }
}