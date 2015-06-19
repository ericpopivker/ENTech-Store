using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.QueryExecuter
{
	public interface IRepository<T> where T : IEntity 
	{
		void Add(T entity);
		T GetById(int entityId);
	}
}