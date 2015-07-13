using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public interface IRepository<T> where T : IEntity 
	{
		void Add(T entity);
		void Add(IEnumerable<T> entity);

		T GetById(int entityId);
		IEnumerable<T> FindByIds(IEnumerable<int> entityIds);

		/// <summary>
		/// Delete entity. Deletes entity from DB or marks entity as deleted for ILogicallyDeletable entities.
		/// </summary>
		/// <exception cref="EntityDeletedException">Throws when entity is already deleted. Applies to ILogicallyDeletable entities.</exception>
		/// <param name="entity">Entity to delete.</param>
		void Delete(T entity);

		/// <summary>
		/// Delete entity. Deletes entity from DB or marks entity as deleted for ILogicallyDeletable entities.
		/// </summary>
		/// <exception cref="EntityDeletedException">Throws when entity is already deleted. Applies to ILogicallyDeletable entities.</exception>
		/// <param name="entities">Collection of entities to delete.</param>
		void Delete(IEnumerable<T> entities);

		void Update(T entity);

		void Update(IEnumerable<T> entities);
	}
}