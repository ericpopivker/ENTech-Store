using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class Repository<TEntity, TDbEntity> : IRepository<TEntity> 
		where TEntity : class, IDomainEntity
		where TDbEntity : class, IDbEntity
	{
		private readonly IDbContext _dbContext;
		private readonly IDbSet<TDbEntity> _dbSet;
		private readonly IDbEntityStateKeeper<TEntity, TDbEntity> _dbEntityStateKeeper;
		private readonly IDbEntityMapper _dbEntityMapper;

		public Repository(
			IDbContext dbContext,
			IDbSet<TDbEntity> dbSet, 
			IDbEntityStateKeeper<TEntity, TDbEntity> dbEntityStateKeeper, 
			IDbEntityMapper dbEntityMapper)
		{
			_dbContext = dbContext;
			_dbSet = dbSet;
			_dbEntityStateKeeper = dbEntityStateKeeper;
			_dbEntityMapper = dbEntityMapper;
		}

		public void Add(TEntity entity)
		{
			//TODO migrate to Strategy when needed
			if (entity is IAuditable)
			{
				HandleAuditableCreatedAt((IAuditable) entity);
			}

			var dbEntity = _dbEntityMapper.CreateDbEntity<TEntity, TDbEntity>(entity);

			_dbSet.Add(dbEntity);

			//THIS IS BECAUSE WE USE 1 DB CONTEXT FOR EVERYTHING, NOT DISTRIBUTED TRANSACTIONS
			_dbContext.SaveChanges();
			entity.Id = dbEntity.Id;

			_dbEntityStateKeeper.Store(entity, dbEntity);
		}

		public void Add(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				if (entity is IAuditable)
				{
					HandleAuditableCreatedAt((IAuditable) entity);
				}

				var dbEntity = _dbEntityMapper.CreateDbEntity<TEntity, TDbEntity>(entity);

				_dbSet.Add(dbEntity);
				_dbEntityStateKeeper.Store(entity, dbEntity);
			}

			_dbContext.SaveChanges();

			foreach (var entity in entities)
			{
				var dbEntity = _dbEntityStateKeeper.Get(entity);
				entity.Id = dbEntity.Id;
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
			var dbEntity = _dbEntityStateKeeper.Get(entity);

			if (dbEntity == null)
				throw new EntityPersistenceException();

			//TODO migrate to Strategy when needed
			if (dbEntity is ILogicallyDeletable)
			{
				var castEntity = (ILogicallyDeletable) dbEntity;

				HandleLogicallyDeletable(castEntity);

				if (entity is ILogicallyDeletable)
					HandleLogicallyDeletable((ILogicallyDeletable)entity);

				_dbEntityMapper.ApplyChanges(entity, dbEntity);
			}
			else
			{
				_dbSet.Remove(dbEntity);
			}

			_dbEntityStateKeeper.Remove(entity);

			_dbContext.SaveChanges();
		}

		public void Delete(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				var dbEntity = _dbEntityStateKeeper.Get(entity);

				if (dbEntity == null)
					throw new EntityPersistenceException();

				//TODO migrate to Strategy when needed
				if (dbEntity is ILogicallyDeletable)
				{
					HandleLogicallyDeletable((ILogicallyDeletable)dbEntity);

					if (entity is ILogicallyDeletable)
						HandleLogicallyDeletable((ILogicallyDeletable)entity);

					_dbEntityMapper.ApplyChanges(entity, dbEntity);
				}
				else
				{
					_dbSet.Remove(dbEntity);
				}

				_dbEntityStateKeeper.Remove(entity);
			}

			_dbContext.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			var dbEntity = _dbEntityStateKeeper.Get(entity);

			if (dbEntity == null)
				throw new EntityPersistenceException();

			//TODO migrate to Strategy when needed
			if (dbEntity is IAuditable)
			{
				HandleAuditableUpdatedAt((IAuditable) dbEntity);

				if (entity is IAuditable)
					HandleAuditableUpdatedAt((IAuditable) entity);
			}

			_dbEntityMapper.ApplyChanges(entity, dbEntity);
			
			//THIS IS BECAUSE WE USE 1 DB CONTEXT FOR EVERYTHING, NOT DISTRIBUTED TRANSACTIONS
			_dbContext.SaveChanges();
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				var dbEntity = _dbEntityStateKeeper.Get(entity);

				if (dbEntity == null)
					throw new EntityPersistenceException();

				//TODO migrate to Strategy when needed
				if (dbEntity is IAuditable)
				{
					HandleAuditableUpdatedAt((IAuditable)dbEntity);

					if (entity is IAuditable)
						HandleAuditableUpdatedAt((IAuditable)entity);
				}

				_dbEntityMapper.ApplyChanges(entity, dbEntity);
			}

			_dbContext.SaveChanges();
		}

		private static void HandleLogicallyDeletable(ILogicallyDeletable castEntity)
		{
			if (castEntity.IsDeleted)
			{
				throw new EntityDeletedException();
			}

			castEntity.IsDeleted = true;
			castEntity.DeletedAt = DateTime.UtcNow;
		}

		private static void HandleAuditableUpdatedAt(IAuditable dbEntity)
		{
			dbEntity.LastUpdatedAt = DateTime.UtcNow;
		}

		private static void HandleAuditableCreatedAt(IAuditable entity)
		{
			var now = DateTime.UtcNow;

			entity.CreatedAt = now;
			entity.LastUpdatedAt = now;
		}
	}
}