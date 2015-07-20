using System.Data.Entity;
using System.Web.Http;
using ENTech.Store.Api.App_Data;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.AuthenticationModule;
using ENTech.Store.Services.CommandService;
using ENTech.Store.Services.CommandService.Concrete;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
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
				.RegisterType<IDbEntityMapper, DbEntityMapper>()
				.RegisterType<IPartnerQuery, PartnerQuery>()
				.RegisterType<IRepository<Entities.StoreModule.Store>, Repository<Entities.StoreModule.Store, StoreDbEntity>>()
				.RegisterType<IRepository<Partner>, Repository<Partner, PartnerDbEntity>>()
				.RegisterType<IRepository<Address>, Repository<Address, AddressDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<Entities.StoreModule.Store, StoreDbEntity>, DbEntityStateKeeper<Entities.StoreModule.Store, StoreDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<Partner, PartnerDbEntity>, DbEntityStateKeeper<Partner, PartnerDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<Address, AddressDbEntity>, DbEntityStateKeeper<Address, AddressDbEntity>>()
				.RegisterType<IDbSet<StoreDbEntity>>(new InjectionFactory(c => DbContextScope.CurrentDbContext.Stores))	//try reflection	
				.RegisterType<IDbSet<PartnerDbEntity>>(new InjectionFactory(c => DbContextScope.CurrentDbContext.Partners))	//try reflection	
				.RegisterType<IDbSet<AddressDbEntity>>(new InjectionFactory(c => DbContextScope.CurrentDbContext.Addresses))	//try reflection	
				.RegisterType<IDbContext>(new InjectionFactory(c => c.Resolve<IDbContextFactory>().Create()));			

			config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}