using System.Linq;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> 
		where TEntity : class, IEntity
	{
		private readonly IDbSetResolver _dbSetResolver;

		public Repository(IDbSetResolver dbSetResolver)
		{
			_dbSetResolver = dbSetResolver;
		}

		public void Add(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public TEntity GetById(int entityId)
		{
			var dbSet = _dbSetResolver.Resolve<TEntity>();
			return dbSet.FirstOrDefault(x => x.Id == entityId);
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