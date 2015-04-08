using GM.Services.Utilisateurs;
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
            //var repo2 = Mock.Create<>();
            //var controller = new AccountController(repo);
            //var model = new RegisterModel();
            //var result = (ViewResult)controller.Inscription(model);
            //Mapper.CreateMap<Utilisateur, RegisterModel>();
            //var user = AutoMapper.Mapper.Map<Utilisateur>(model);




        }
        

        
    }
}
