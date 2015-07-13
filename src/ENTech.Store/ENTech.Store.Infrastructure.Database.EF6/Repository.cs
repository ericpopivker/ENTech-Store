using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class Repository<TEntity> : IRepository<TEntity> 
		where TEntity : class, IEntity
	{
		private readonly IDbSet<TEntity> _dbSet;
		private readonly IDbEntityStateManager<TEntity> _dbEntityStateManager;

		public Repository(IDbSet<TEntity> dbSet, IDbEntityStateManager<TEntity> dbEntityStateManager)
		{
			if (dbSet == null)
				throw new ArgumentNullException("dbSet");

			if (dbEntityStateManager == null)
				throw new ArgumentNullException("dbEntityStateManager");

			_dbSet = dbSet;
			_dbEntityStateManager = dbEntityStateManager;
		}

		public void Add(TEntity entity)
		{
			//TODO migrate to Strategy when needed
			if (entity is IAuditable)
			{
				var castEntity = (IAuditable)entity;

				var now = DateTime.UtcNow;

				castEntity.CreatedAt = now;
				castEntity.LastUpdatedAt = now;
			}

			_dbSet.Add(entity);
		}

		public void Add(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				Add(entity);
			}
		}

		public TEntity GetById(int entityId)
		{
			return _dbSet.FirstOrDefault(x => x.Id == entityId);
		}

		public IEnumerable<TEntity> FindByIds(IEnumerable<int> entityIds)
		{
			var results = _dbSet.Where(x => entityIds.Contains(x.Id));

			var resultIds = results.Select(x => x.Id);
			if (entityIds.Except(resultIds).Any())
				throw new EntityLoadMismatchException();

			return results;
		}

		public void Delete(TEntity entity)
		{
			//TODO migrate to Strategy when needed
			if (entity is ILogicallyDeletable)
			{
				var castEntity = (ILogicallyDeletable) entity;
				if (castEntity.IsDeleted)
				{
					throw new EntityDeletedException();
				}

				castEntity.IsDeleted = true;
				castEntity.DeletedAt = DateTime.UtcNow;
				_dbEntityStateManager.MarkUpdated(entity);
			}
			else
			{
				_dbSet.Remove(entity);
			}
		}

		public void Delete(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				Delete(entity);
			}
		}

		public void Update(TEntity entity)
		{
			//TODO migrate to Strategy when needed
			if (entity is IAuditable)
			{
				var castEntity = (IAuditable)entity;
				castEntity.LastUpdatedAt = DateTime.UtcNow;
			}

			_dbEntityStateManager.MarkUpdated(entity);
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				Update(entity);
			}
		}
	}
}