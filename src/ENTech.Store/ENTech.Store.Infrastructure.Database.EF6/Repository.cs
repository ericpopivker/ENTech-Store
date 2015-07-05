using System;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Database.Repository.Exceptions;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class Repository<TEntity> : IRepository<TEntity> 
		where TEntity : class, IEntity
	{
		private readonly IDbSetResolver _dbSetResolver;
		
		private IDbSet<TEntity> DbSet
		{
			get
			{
				var dbSet = _dbSetResolver.Resolve<TEntity>();
				if (dbSet == null)
					throw new NonPersistentEntityException<TEntity>();
				return dbSet;
			}
		}

		public Repository(IDbSetResolver dbSetResolver)
		{
			_dbSetResolver = dbSetResolver;
		}

		public void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public TEntity GetById(int entityId)
		{
			return DbSet.FirstOrDefault(x => x.Id == entityId);
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