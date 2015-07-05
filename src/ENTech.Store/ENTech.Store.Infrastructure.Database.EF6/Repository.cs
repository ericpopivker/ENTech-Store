using System;
using System.Linq;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
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
			throw new NotImplementedException();
		}

		public TEntity GetById(int entityId)
		{
			var dbSet = _dbSetResolver.Resolve<TEntity>();
			return dbSet.FirstOrDefault(x => x.Id == entityId);
		}

		public void Delete(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Update(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}