using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository.Decorators
{
	public class L3CacheRepositoryDecorator<TEntity> : IRepository<TEntity> where TEntity : class, IDomainEntity, IAuditable
	{
		private IRepository<TEntity> _dbRepository;
		private IDistributedCache _l3Cache;

		public L3CacheRepositoryDecorator(IRepository<TEntity> dbRepository, IDistributedCache l3Cache)
		{
			_dbRepository = dbRepository;
			_l3Cache = l3Cache;
		}

		public void Add(TEntity entity)
		{
			_dbRepository.Add(entity);

			AddOrUpdateEntityInCache(entity);
		}

		public void Add(IEnumerable<TEntity> entities)
		{
			_dbRepository.Add(entities);

			AddOrUpdateEntitiesInCache(entities);
		}


		public void Update(TEntity entity)
		{
			_dbRepository.Add(entity);

			AddOrUpdateEntityInCache(entity);
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			_dbRepository.Add(entities);

			AddOrUpdateEntitiesInCache(entities);
		}


		public void Delete(TEntity entity)
		{
			_dbRepository.Delete(entity);

			DeleteEntityFromCache(entity.Id);
		}

		public void Delete(IEnumerable<TEntity> entities)
		{
			_dbRepository.Delete(entities);

			DeleteEntitiesFromCache(entities.Select(entity => entity.Id));
		}


		public TEntity GetById(int entityId)
		{
			var entityCacheKey = GetEntityCacheKey(entityId);

			TEntity entity = null;
			bool existsInL3Cache = _l3Cache.TryGet(entityCacheKey, ref entity);

			if (existsInL3Cache)
			{
				return entity;
			}

			entity = _dbRepository.GetById(entityId);

			AddOrUpdateEntityInCache(entity);

			return entity;
		}

		public IEnumerable<TEntity> FindByIds(IEnumerable<int> entityIds)
		{
			var entityKeys = entityIds.ToDictionary(GetEntityCacheKey);

			var entities = new List<TEntity>();
			var entityIdsForDb = new List<int>();

			//Get L3 LastUpdatedAt
			IDictionary<string, TEntity> dicEntities = _l3Cache.FindByKeys<TEntity>(entityKeys.Keys);
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
				var entitiesFromDb = _dbRepository.FindByIds(entityIdsForDb);
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
			return _dbRepository.GetEntityMetaState(entityId);
		}




		private void AddOrUpdateEntityInCache(TEntity entity)
		{
			var entityId = entity.Id;

			Verify.Argument.IsPositive(entityId, "entitityId");
			Verify.Argument.IsNotNull(entity, "entity");

			var entityCacheKey = GetEntityCacheKey(entityId);
			var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);

			_l3Cache.Set(entityCacheKey, entity);
			_l3Cache.Set(lastUpdatedAtCacheKey, entity.LastUpdatedAt.Ticks.ToString());

		}


		private void AddOrUpdateEntitiesInCache(IEnumerable<TEntity> entities)
		{
			var entityTuples = new List<Tuple<string, TEntity>>();
			var lastUpdatedAtTuples = new List<Tuple<string, string>>();

			foreach (var entity in entities)
			{
				var entityCacheKey = GetEntityCacheKey(entity.Id);

				var entityTuple = new Tuple<string, TEntity>(entityCacheKey, entity);
				entityTuples.Add(entityTuple);

				var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entity.Id);

				var lastUpdatedAtTuple = new Tuple<string, string>(lastUpdatedAtCacheKey, entity.LastUpdatedAt.Ticks.ToString());
				lastUpdatedAtTuples.Add(lastUpdatedAtTuple);
			}

			_l3Cache.Set(entityTuples);
			_l3Cache.Set(lastUpdatedAtTuples);

		}


		private void DeleteEntityFromCache(int entityId)
		{
			Verify.Argument.IsPositive(entityId, "entitityId");

			var entityCacheKey = GetEntityCacheKey(entityId);
			var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);

			_l3Cache.TryRemove(entityCacheKey);
			_l3Cache.TryRemove(lastUpdatedAtCacheKey);
		}



		private void DeleteEntitiesFromCache(IEnumerable<int> entityIds)
		{
			var entityCacheKeys = new List<string>();
			var lastUpdatedAtCachedKeys = new List<string>();

			foreach (var entityId in entityIds)
			{
				var entityCacheKey = GetEntityCacheKey(entityId);
				var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);

				entityCacheKeys.Add(entityCacheKey);
				lastUpdatedAtCachedKeys.Add(lastUpdatedAtCacheKey);
			}

			_l3Cache.Remove(entityCacheKeys);
			_l3Cache.Remove(lastUpdatedAtCachedKeys);

		}



		public string GetLastUpdateAtCacheKey(int entityId)
		{
			string key = String.Format("{0}_LastUpdatedAt", GetEntityCacheKey(entityId));
			return key;
		}


		public string GetEntityCacheKey(int entityId)
		{
			string key = String.Format("{0}_{1}", typeof(TEntity).FullName, entityId);
			return key;
		}


		public DateTime GetEntityLastUpdatedAt(int entityId)
		{
			var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);

			string l3LastUpdatedAtTicks = null;
			bool existsInL3Cache = _l3Cache.TryGet(lastUpdatedAtCacheKey, ref l3LastUpdatedAtTicks);

			if (existsInL3Cache)
			{
				DateTime l3LastUpdatedAt = new DateTime(long.Parse(l3LastUpdatedAtTicks));
				return l3LastUpdatedAt;
			}

			TEntity entity = GetById(entityId);
			return entity.LastUpdatedAt;
		}

	}
	
}
