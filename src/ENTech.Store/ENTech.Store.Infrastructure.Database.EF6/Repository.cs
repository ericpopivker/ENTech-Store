using System;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class Repository<TEntity> : IRepository<TEntity> 
		where TEntity : class, IEntity
	{
		private readonly IDbSet<TEntity> _dbSet;

		public Repository(IDbSet<TEntity> dbSet)
		{
			if (dbSet == null)
				throw new ArgumentNullException("dbSet");

			_dbSet = dbSet;
		}

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public TEntity GetById(int entityId)
		{
			return _dbSet.FirstOrDefault(x => x.Id == entityId);
		}

		public void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void Update(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}