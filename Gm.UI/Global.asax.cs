using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using GM.Core.Models;
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
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            Mapper.CreateMap<RegisterModel, Utilisateur>()
                .ForMember(x => x.UtilisateurRoles, o => o.MapFrom(x => x.UtilisateurRoles));
            Mapper.CreateMap<Pharmacie, PharmacieModel>().ForMember(x => x.Logo, o => o.MapFrom(x => x.LogoUrl));
            Mapper.CreateMap<PharmacieModel, Pharmacie>().ForMember(x => x.LogoUrl, o => o.MapFrom(x => x.Logo));
            Mapper.CreateMap<Utilisateur, RegisterModel>();
            Mapper.CreateMap<MedicamentModel, Medicament>();
            Mapper.CreateMap<Medicament, MedicamentModel>()
                .ForMember(x => x.Dci, o => o.MapFrom(x => x.Dci.Nom))
                .ForMember(x => x.Specialite, o => o.MapFrom(x => x.Specialite.Libelle))
                .ForMember(x => x.Forme, o => o.MapFrom(x => x.Forme.Libelle))
                .ForMember(x => x.Conditionnement, o => o.MapFrom(x => x.Conditionnement.Libelle))
            .ForMember(x => x.TarifReference, o => o.MapFrom(x => x.Remboursements.OrderBy(r => r.Date).FirstOrDefault().TarifReference)); 
            
        }
    }
}
