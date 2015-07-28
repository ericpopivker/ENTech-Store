using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Database.Utility
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