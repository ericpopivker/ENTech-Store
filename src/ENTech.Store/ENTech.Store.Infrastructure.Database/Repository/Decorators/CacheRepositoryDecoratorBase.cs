using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository.Decorators
{
	public abstract class CacheRepositoryDecoratorBase<TEntity> : ICachedRepository<TEntity> where TEntity : class, IDomainEntity, IAuditable
	{
		protected IRepository<TEntity> _baseRepository;
		protected ICache _cache;

		public CacheRepositoryDecoratorBase(IRepository<TEntity> baseRepository, ICache cache)
		{
			_baseRepository = baseRepository;
			_cache = cache;
		}

		public void Add(TEntity entity)
		{
			_baseRepository.Add(entity);

			AddOrUpdateEntityInCache(entity);
		}

		public void Add(IEnumerable<TEntity> entities)
		{
			_baseRepository.Add(entities);

			AddOrUpdateEntitiesInCache(entities);
		}


		public void Update(TEntity entity)
		{
			_baseRepository.Add(entity);

			AddOrUpdateEntityInCache(entity);
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			_baseRepository.Add(entities);

			AddOrUpdateEntitiesInCache(entities);
		}


		public void Delete(TEntity entity)
		{
			_baseRepository.Delete(entity);

			DeleteEntityFromCache(entity.Id);
		}

		public void Delete(IEnumerable<TEntity> entities)
		{
			_baseRepository.Delete(entities);

			DeleteEntitiesFromCache(entities.Select(entity => entity.Id));
		}


		public TEntity GetById(int entityId)
		{
			var entityCacheKey = CacheKey.ForEntity<TEntity>(entityId);

			TEntity entity = null;
			bool existsInCache = _cache.TryGet(entityCacheKey, ref entity);

			if (existsInCache)
			{
				return entity;
			}

			entity = _baseRepository.GetById(entityId);

			AddOrUpdateEntityInCache(entity);

			return entity;
		}

		public IEnumerable<TEntity> FindByIds(IEnumerable<int> entityIds)
		{
			var entityKeys = entityIds.ToDictionary(CacheKey.ForEntity<TEntity>);

			var entities = new List<TEntity>();
			var entityIdsForDb = new List<int>();

			IDictionary<string, TEntity> dicEntities = _cache.FindByKeys<TEntity>(entityKeys.Keys);
			foreach (var key in dicEntities.Keys)
			{
				var entity = dicEntities[key];
				if (entity != null)
				{
					entities.Add(entity);
				}
				else
				{
					entityIdsForDb.Add(entityKeys[key]);
				}
			}

			if (entityIdsForDb.Count > 0)
			{
				var entitiesFromDb = _baseRepository.FindByIds(entityIdsForDb);
				if (entityIdsForDb.Count != entitiesFromDb.Count())
				{
					var foundEntityIds = entitiesFromDb.Select(entity => entity.Id).ToList();
					var notFoundEntityIds = entityIdsForDb.Except(foundEntityIds);

					string errorMessage = "Entities of type '" + typeof(TEntity).Name + "' with Ids [" +
										  String.Join(", ", notFoundEntityIds) + "] do not exist";

					throw new InvalidOperationException(errorMessage);
				}

				AddOrUpdateEntitiesInCache(entitiesFromDb);

				entities.AddRange(entitiesFromDb);
			}

			entities = SortByIds(entities, entityIds);
			return entities;
		}

		private List<TEntity> SortByIds(IEnumerable<TEntity> fromEntities, IEnumerable<int> entityIds)
		{
			var toEntities = new List<TEntity>();
			var dic = fromEntities.ToDictionary(entity => entity.Id);

			foreach (int entityId in entityIds)
			{
				toEntities.Add(dic[entityId]);
			}

			return toEntities;
		}


		public EntityMetaState GetEntityMetaState(int entityId)
		{
			return _baseRepository.GetEntityMetaState(entityId);
		}


		protected virtual void AddOrUpdateEntityInCache(TEntity entity)
		{
			var entityId = entity.Id;

			Verify.Argument.IsPositive(entityId, "entitityId");
			Verify.Argument.IsNotNull(entity, "entity");

			var entityCacheKey = CacheKey.ForEntity<TEntity>(entityId);
			_cache.Set(entityCacheKey, entity);
		}


		protected virtual void AddOrUpdateEntitiesInCache(IEnumerable<TEntity> entities)
		{
			var entityTuples = new List<Tuple<string, TEntity>>();

			foreach (var entity in entities)
			{
				var entityCacheKey = CacheKey.ForEntity<TEntity>(entity.Id);

				var entityTuple = new Tuple<string, TEntity>(entityCacheKey, entity);
				entityTuples.Add(entityTuple);
			}

			_cache.Set(entityTuples);
		}


		protected virtual void DeleteEntityFromCache(int entityId)
		{
			Verify.Argument.IsPositive(entityId, "entitityId");

			var entityCacheKey = CacheKey.ForEntity<TEntity>(entityId);
			_cache.TryRemove(entityCacheKey);
		}


		protected virtual void DeleteEntitiesFromCache(IEnumerable<int> entityIds)
		{
			var entityCacheKeys = new List<string>();

			foreach (var entityId in entityIds)
			{
				var entityCacheKey = CacheKey.ForEntity<TEntity>(entityId);
				entityCacheKeys.Add(entityCacheKey);
			}

			_cache.Remove(entityCacheKeys);
		}



		protected virtual DateTime GetEntityLastUpdatedAt(int entityId)
		{
			TEntity entity = GetById(entityId);
			return entity.LastUpdatedAt;
		}

	}
}
