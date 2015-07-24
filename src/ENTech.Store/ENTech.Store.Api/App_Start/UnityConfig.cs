using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.AuthenticationModule;
using ENTech.Store.Services.CommandService;
using ENTech.Store.Services.CommandService.Concrete;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.EntityValidators;
using ENTech.Store.Services.Misc;
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

	        container.RegisterType<ICommandFactory, CommandFactory>()
				.RegisterType<IUnitOfWork, UnitOfWork>()
				.RegisterType<IDbContextFactory, DbContextFactory>()
		        .RegisterType<IExternalCommandService<AnonymousSecurityInformation>, PublicExternalCommandService>()
				.RegisterType<IExternalCommandService<BusinessAdminSecurityInformation>, BusinessAdminExternalCommandService>()
				.RegisterType<IInternalCommandService, InternalCommandService>()
				.RegisterType<IMapper, Mapper>()
				.RegisterType<IDtoValidatorFactory, DtoValidatorFactory>()
				.RegisterType<IDbEntityMapper, DbEntityMapper>()
				.RegisterType<IPartnerQuery, PartnerQuery>()
				.RegisterType<IStoreValidator, StoreValidator>()
				.RegisterType<IAddressValidator, AddressValidator>()
				.RegisterType<IProductValidator, ProductValidator>()

				.RegisterEntityForRepository<Entities.StoreModule.Store, StoreDbEntity>()
				.RegisterEntityForRepository<Partner, PartnerDbEntity>()
				.RegisterEntityForRepository<Address, AddressDbEntity>()
				.RegisterEntityForRepository<Product, ProductDbEntity>()
				.RegisterType<IDbContext>(new InjectionFactory(c => DbContextScope.CurrentDbContext));			

			config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }


	public static class UnityExtensions
	{
		public static IUnityContainer RegisterEntityForRepository<TEntity, TDbEntity>(this IUnityContainer unityContainer) where TEntity : class, IDomainEntity where TDbEntity : class, IDbEntity
		{
			unityContainer
				.RegisterType<IRepository<TEntity>, Repository<TEntity, TDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<TEntity, TDbEntity>, DbEntityStateKeeper<TEntity, TDbEntity>>();

			return unityContainer;
		}
	}
}