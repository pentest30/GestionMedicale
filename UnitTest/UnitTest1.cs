using System.Web.Mvc;
using GM.Core;
using GM.Services.Utilisateurs;
using Gm.UI.Controllers;
using Gm.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repo = Mock.Create<IServiceUtilisateur>();
            var controller = new AccountController(repo);
            var model = new RegisterModel();
            var result = (ViewResult)controller.Inscription(model);
            //Mapper.CreateMap<Utilisateur, RegisterModel>();
            //var user = AutoMapper.Mapper.Map<Utilisateur>(model);




        }
        

        
    }
}
