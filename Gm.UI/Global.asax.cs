using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using GM.Core.Models;
using Gm.UI.Models;
using Gm.UI.Models.Utilisateurs;

namespace Gm.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            Mapper.CreateMap<RegisterModel, Utilisateur>()
                .ForMember(x => x.UtilisateurRoles, o => o.MapFrom(x => x.UtilisateurRoles));
            Mapper.CreateMap<Utilisateur, RegisterModel>();
            //BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
           // BootstrapMvcSample.ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
