using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public interface IRepository<T> where T : IEntity 
	{
		void Add(T entity);
		T GetById(int entityId);
		void Delete(T entity);
		void Update(T entity);
	}
}