using System.Collections.Generic;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Services.Repositories
{
	public enum EntityMetaState
	{
		NotFound,
		Exists,
		Deleted
	}


	public interface IRepository<T> where T : IEntity
	{
		void Add(T entity);

		void Update(T entity);
		
		T GetById(int entityId);

		T GetById(IList<int> entityIds);

		void Delete(int entityId);


		EntityMetaState GetEntityMetaState(int entityId);
	}
}
