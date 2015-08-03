using ENTech.Store.Database;
using ENTech.Store.Database.Utility;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Database.Repository.Decorators;
using ENTech.Store.Infrastructure.Entities;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Api.Extensions
{
	public static class UnityExtensions
	{
		public static IUnityContainer RegisterEntityForRepository<TEntity, TDbEntity>(this IUnityContainer unityContainer) where TEntity : class, IDomainEntity where TDbEntity : class, IDbEntity
		{
			unityContainer
				.RegisterType<IRepository<TEntity>, Repository<TEntity, TDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<TEntity, TDbEntity>, DbEntityStateKeeper<TEntity, TDbEntity>>();

			return unityContainer;
		}
		public static IUnityContainer RegisterEntityForCachedRepository<TEntity, TDbEntity>(this IUnityContainer unityContainer) where TEntity : class, IDomainEntity, IAuditable where TDbEntity : class, IDbEntity
		{
			unityContainer
				.RegisterType<ICachedRepository<TEntity>, L3CacheRepositoryDecorator<TEntity>>()
				.RegisterType<IRepository<TEntity>, Repository<TEntity, TDbEntity>>()
				.RegisterType<IDbEntityStateKeeper<TEntity, TDbEntity>, DbEntityStateKeeper<TEntity, TDbEntity>>();

			return unityContainer;
		}
	}
}