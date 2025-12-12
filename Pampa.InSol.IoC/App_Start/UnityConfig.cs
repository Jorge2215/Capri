using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Biz.Servicios;
using Pampa.InSol.Biz.Servicios.Negocio;
using Pampa.InSol.Common.Logging;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Dal.Soporte;
using Pampa.InSol.Entities.Entities;
using System;

namespace Pampa.InSol.IoC.App_Start
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
            container.AddNewExtension<Interception>();
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

        public static T Resolve<T>()
        {
            return GetConfiguredContainer().Resolve<T>();
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
            RegisterRelatedToDatabase(container);

            RegisterRelatedToServices(container);
        }

        private static void RegisterRelatedToDatabase(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
        }

        private static void RegisterRelatedToServices(IUnityContainer container)
        {
            container.RegisterType<IUsuarioServicio, UsuarioServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IRolServicio, RolServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IDominioServicio, DominioServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IStoredProcedureInvoker, UnitOfWork>(new PerRequestLifetimeManager(), new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingInterceptionBehavior>());

            container.RegisterType<IProductoServicio, ProductoServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ICicloAplicativoServicio, CicloAplicativoServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IObsolescenciaServicio, ObsolescenciaServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ISitioServicio, SitioServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ITipoProductoServicio, TipoProductoServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IServicioServicio, ServicioServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IAmbienteServicio, AmbienteServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IInterfazServicio, InterfazServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IModuloServicio, ModuloServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ITransporteServicio, TransporteServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ITecnologiaServicio, TecnologiaServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ITipoInterfazServicio, TipoInterfazServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IFrecuenciaServicio, FrecuenciaServicio>(new PerRequestLifetimeManager());
            container.RegisterType<ITipoDatoServicio, TipoDatoServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IRolAmbienteServicio, RolAmbienteServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IProductoAmbienteServicio, ProductoAmbienteServicio>(new PerRequestLifetimeManager());

            container.RegisterType<IServicio<Negocio>, NegocioServicio>(new PerRequestLifetimeManager());
            container.RegisterType<IServicio<Proceso>, ProcesoServicio>(new PerRequestLifetimeManager());
        }
    }
}