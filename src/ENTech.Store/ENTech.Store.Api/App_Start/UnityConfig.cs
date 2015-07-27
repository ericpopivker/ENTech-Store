using System.Linq;
using System.Web.Http;
using ENTech.Store.Api.Extensions;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.AuthenticationModule;
using ENTech.Store.Services.CommandService;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.EntityValidators;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace ENTech.Store.Api
{
    public static class UnityConfig
    {
		public static void RegisterComponents(HttpConfiguration config)
        {
			var container = IoC.Container;

			container
				//DB infrastructure
				.RegisterType<ICommandFactory, CommandFactory>()
				.RegisterType<IUnitOfWork, UnitOfWork>()
				.RegisterType<IDbContextFactory, DbContextFactory>()
				.RegisterType<IDbEntityMapper, DbEntityMapper>()
				.RegisterType<IExternalCommandService, ExternalCommandService>()
				.RegisterType<IInternalCommandService, InternalCommandService>()

				//common infrastructure
				.RegisterType<IMapper, Mapper>()
				.RegisterType<IDtoValidatorFactory, DtoValidatorFactory>()

				//validators
				.RegisterType<IStoreValidator, StoreValidator>()
				.RegisterType<IAddressValidator, AddressValidator>()
				.RegisterType<IProductValidator, ProductValidator>()

				//queries
				.RegisterType<IPartnerQuery, PartnerQuery>()

				//repositories
				.RegisterEntityForRepository<Entities.StoreModule.Store, StoreDbEntity>()
				.RegisterEntityForRepository<Partner, PartnerDbEntity>()
				.RegisterEntityForRepository<Address, AddressDbEntity>()
				.RegisterEntityForRepository<Product, ProductDbEntity>()
				.RegisterType<IDbContext>(new InjectionFactory(c => DbContextScope.CurrentDbContext))

				//commands
				.RegisterTypes(
					AllClasses.FromLoadedAssemblies().
						Where(type => typeof (IInternalCommand).IsAssignableFrom(type)),
					type => type.GetInterfaces()
						.Where(x => x.Name == typeof(ICommand<,>).Name 
							&& x.Namespace == typeof(ICommand<,>).Namespace)); //TODO: cleanup, HACKISH


			config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}