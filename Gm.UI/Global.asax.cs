using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using GM.Core.Models;
using Gm.UI.Models;

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
            Mapper.CreateMap<RegisterModel, Utilisateur>();
                //.ForMember(x => x.UtilisateurRoles.FirstOrDefault().RoleId, o => o.MapFrom(x => x.RoleId));
            Mapper.CreateMap<Utilisateur, RegisterModel>();
        }
    }
}
