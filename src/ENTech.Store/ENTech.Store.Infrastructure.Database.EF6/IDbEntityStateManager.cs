using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IDbEntityStateManager<TEntity> where TEntity : IEntity
	{
		void MarkUpdated(TEntity stubEntity);
	}
}