using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database
{
	public interface IRepository<T> where T : IEntity 
	{
		void Add(T entity);
		T GetById(int entityId);
	}
}