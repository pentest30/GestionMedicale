using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Gm.UI.Areas.Gestion.Models;
using Gm.UI.Models.Docteurs;
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
            Mapper.CreateMap<Fournisseur, PharmacieModel>().ForMember(x => x.Logo, o => o.MapFrom(x => x.LogoUrl));
            Mapper.CreateMap<PharmacieModel, Fournisseur>().ForMember(x => x.LogoUrl, o => o.MapFrom(x => x.Logo));
            Mapper.CreateMap<LigneCommande, LigneComamndeModel>();
            Mapper.CreateMap<LigneComamndeModel, LigneCommande>();
            Mapper.CreateMap<Utilisateur, RegisterModel>();
            Mapper.CreateMap<MedicamentModel, Medicament>();
            Mapper.CreateMap<Utilisateur, DocteurModel>();
            Mapper.CreateMap<DocteurModel, Utilisateur>();
            Mapper.CreateMap<Stock, StockModel>().ForMember(x => x.NomCommerciale, o => o.MapFrom(x => x.Medicament.NomCommerciale))
                .ForMember(x => x.Dose, o => o.MapFrom(x => x.Medicament.Dose))
                 .ForMember(x => x.Magasin, o => o.MapFrom(x => x.Magasin.Libelle))
                .ForMember(x => x.Forme, o => o.MapFrom(x => x.Medicament.Forme.Libelle))
                .ForMember(x => x.QntCritique, o => o.MapFrom(x => x.Medicament.ParamStocks.FirstOrDefault(m => m.EntrepriseId == x.EntrepriseId).QntCritique))
                .ForMember(x => x.NomCommerciale, o => o.MapFrom(x => x.Medicament.NomCommerciale));
            Mapper.CreateMap<StockModel, Stock>();
             
            Mapper.CreateMap<Entree, BonEntree>(); 
            Mapper.CreateMap<BonEntree, Entree>();
            Mapper.CreateMap<LigneEntreeMagasin, LigneEntree>();
            Mapper.CreateMap<LigneEntree, LigneEntreeMagasin>();
            Mapper.CreateMap<Medicament, MedicamentModel>()
                .ForMember(x => x.Dci, o => o.MapFrom(x => x.Dci.Nom))
                .ForMember(x => x.Specialite, o => o.MapFrom(x => x.Specialite.Libelle))
                .ForMember(x => x.Forme, o => o.MapFrom(x => x.Forme.Libelle))
                .ForMember(x => x.Conditionnement, o => o.MapFrom(x => x.Conditionnement.Libelle))
            .ForMember(x => x.TarifReference, o => o.MapFrom(x => x.Remboursements.OrderByDescending(r => r.Date).FirstOrDefault().TarifReference)); 
            
        }
    }
}
