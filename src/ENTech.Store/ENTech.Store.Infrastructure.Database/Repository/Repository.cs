using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> 
		where TEntity : IEntity
	{
		public void Add(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public TEntity GetById(int entityId)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public void Update(TEntity entity)
		{
			throw new System.NotImplementedException();
		}
	}
}