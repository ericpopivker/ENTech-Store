using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class Repository<TDbEntity, TEntity> : IRepository<TEntity> 
		where TEntity : class, IEntity
		where TDbEntity : class, IDbEntity
	{
		private readonly IDbSet<TDbEntity> _dbSet;
		private readonly IDbEntityStateKeeper _dbEntityStateKeeper;
		private readonly IDbEntityMapper _dbEntityMapper;

		public Repository(IDbSet<TDbEntity> dbSet, IDbEntityStateKeeper dbEntityStateKeeper, IDbEntityMapper dbEntityMapper)
		{
			_dbSet = dbSet;
			_dbEntityStateKeeper = dbEntityStateKeeper;
			_dbEntityMapper = dbEntityMapper;
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

			var dbEntity = _dbEntityMapper.CreateDbEntity<TEntity, TDbEntity>(entity);

			_dbSet.Add(dbEntity);

			_dbEntityStateKeeper.Store(entity, dbEntity);
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
			var dbEntity = _dbSet.FirstOrDefault(x => x.Id == entityId);

			var entity = _dbEntityMapper.MapToEntity<TDbEntity, TEntity>(dbEntity);

			_dbEntityStateKeeper.Store(entity, dbEntity);

			return entity;
		}

		public IEnumerable<TEntity> FindByIds(IEnumerable<int> entityIds)
		{
			var dbEntities = _dbSet.Where(x => entityIds.Contains(x.Id));

			var resultIds = dbEntities.Select(x => x.Id);
			if (entityIds.Except(resultIds).Any())
				throw new EntityLoadMismatchException();

			var entities = _dbEntityMapper.MapToEntities<TDbEntity, TEntity>(dbEntities);

			foreach (var dbEntity in dbEntities)
			{
				var entity = entities.Single(x => x.Id == dbEntity.Id);
				_dbEntityStateKeeper.Store(entity, dbEntity);
			}

			return entities;
		}

		public void Delete(TEntity entity)
		{
			var dbEntity = _dbEntityStateKeeper.Get<TEntity, TDbEntity>(entity);

			if (dbEntity == null)
				throw new EntityPersistenceException();

			//TODO migrate to Strategy when needed
			if (dbEntity is ILogicallyDeletable)
			{
				var castEntity = (ILogicallyDeletable) dbEntity;
				if (castEntity.IsDeleted)
				{
					throw new EntityDeletedException();
				}

				castEntity.IsDeleted = true;
				castEntity.DeletedAt = DateTime.UtcNow;

				_dbEntityMapper.ApplyChanges(entity, dbEntity);
			}
			else
			{
				_dbSet.Remove(dbEntity);
			}

			_dbEntityStateKeeper.Remove<TEntity, TDbEntity>(entity);
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
			var dbEntity = _dbEntityStateKeeper.Get<TEntity, TDbEntity>(entity);

			if (dbEntity == null)
				throw new EntityPersistenceException();

			//TODO migrate to Strategy when needed
			if (dbEntity is IAuditable)
			{
				var castEntity = (IAuditable)dbEntity;
				castEntity.LastUpdatedAt = DateTime.UtcNow;
			}

			_dbEntityMapper.ApplyChanges(entity, dbEntity);
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