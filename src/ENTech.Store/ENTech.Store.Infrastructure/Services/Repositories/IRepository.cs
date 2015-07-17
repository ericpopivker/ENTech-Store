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


	public interface IRepository<TEntity> where TEntity : IEntity
	{
		void Add(TEntity entity);

		void Update(TEntity entity);
		
		TEntity GetById(int entityId);

		TEntity FindByIds(IList<int> entityIds);

		void Delete(int entityId);


		EntityMetaState GetEntityMetaState(int entityId);
	}
}
