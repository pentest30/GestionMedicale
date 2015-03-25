using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GM.Core.Models;
using GM.Presentation.Models;

namespace GM.Presentation
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.CreateMap<SimpleUserVm, Utilisateur>();
            Mapper.CreateMap<Utilisateur, SimpleUserVm>();
        }
    }
}
