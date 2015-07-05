using ENTech.Store.Infrastructure.Database.Repository.Exceptions;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public interface IRepository<T> where T : IEntity 
	{
		/// <summary>
		/// </summary>
		/// <exception cref="NonPersistentEntityException{T}">Exception is thrown when entity does not support database operation.</exception>
		/// <param name="entity"></param>
		void Add(T entity);
		T GetById(int entityId);
		void Delete(T entity);
		void Update(T entity);
	}
}