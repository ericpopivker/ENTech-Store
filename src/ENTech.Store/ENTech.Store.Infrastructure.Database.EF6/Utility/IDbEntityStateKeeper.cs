using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public interface IDbEntityStateKeeper<TEntity, TDbEntity>
		where TEntity : IDomainEntity
		where TDbEntity : IDbEntity
	{
		void Store(TEntity entity, TDbEntity dbEntity);

		TDbEntity Get(TEntity entity);

		void Remove(TEntity entity);

		void Clear();
	}
}