using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public interface IDbEntityStateManager<TEntity> where TEntity : IEntity
	{
		void MarkUpdated(TEntity stubEntity);
	}
}