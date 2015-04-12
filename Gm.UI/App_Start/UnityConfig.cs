using System;
using System.Data.Entity;
using GM.Context;
using GM.Core;
using GM.Core.Models;
using GM.Services.Categorie;
using GM.Services.Conditionnelts;
using GM.Services.Formes;
using GM.Services.Nomenclature;
using GM.Services.Pharmacies;
using GM.Services.Utilisateurs;
using Microsoft.Practices.Unity;

namespace Gm.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<DbContext, UserContext>(new PerRequestLifetimeManager());
            container.RegisterType<DbContext, PharmacieContext>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<UtilisateurRole>, RoleUserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Utilisateur>, UtilisateurRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Role>, RoleRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Specialite>, SpecialiteRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Pharmacie>, PharmacieRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Dci>, DciRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Forme>, FormeRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Conditionnement>, ConditionnementRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceUtilisateur,ServiceUtilisateur>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceAdministrateur,ServiceAdministrateur>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceSpecialite, ServiceSpecialite>(new PerRequestLifetimeManager());
            container.RegisterType<IServicePharmacie, ServicePharmacie>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceDci, DciService>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceForme, ServiceForme>(new PerRequestLifetimeManager());
            container.RegisterType<IServiceConditionnement, ServiceConditionnement>(new PerRequestLifetimeManager());
        }
    }
}
