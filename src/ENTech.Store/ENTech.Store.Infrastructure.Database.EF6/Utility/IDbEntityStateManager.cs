using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public interface IDbEntityStateManager
	{
		void MarkUpdated<TEntity>(TEntity stubEntity) where TEntity : class, IEntity;
	}
}