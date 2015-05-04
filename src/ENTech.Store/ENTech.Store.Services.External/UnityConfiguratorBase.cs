using ENTech.Store.Entities;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Services.External
{
	public abstract class UnitConfigBase
	{
		public virtual void RegisterTypes(IUnityContainer container)
		{
			//container.RegisterType<IDbContext, EventGridDbContext>(new InjectionMethod("DisableLazyLoading"));
			container.RegisterType<IDbContext, DbContext>();
			container.RegisterType<IUnitOfWork, UnitOfWork>();
		}
	}
}
