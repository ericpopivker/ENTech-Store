using Microsoft.Practices.Unity;
using System.Web.Http;
using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure;
using ENTech.Store.Services.CommandService;
using ENTech.Store.Services.CommandService.Concrete;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
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
				.RegisterType<IExternalCommandService<BusinessAdminSecurityInformation>, BusinessAdminExternalCommandService>();

			config.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}