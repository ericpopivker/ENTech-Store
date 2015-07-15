using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public interface IDbEntityStateKeeper
	{
		void Store<TEntity, TDbEntity>(TEntity entity, TDbEntity dbEntity)
			where TEntity : IEntity
			where TDbEntity : IDbEntity;

		TDbEntity Get<TEntity, TDbEntity>(TEntity entity)
			where TEntity : IEntity
			where TDbEntity : IDbEntity;

		void Remove<TEntity, TDbEntity>(TEntity entity)
			where TEntity : IEntity
			where TDbEntity : IDbEntity;
	}
}