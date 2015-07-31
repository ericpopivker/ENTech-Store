using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository.Decorators
{
	public class L3CacheRepositoryDecorator<TEntity> : CacheRepositoryDecoratorBase<TEntity> where TEntity : class, IDomainEntity, IAuditable
	{
		public L3CacheRepositoryDecorator(IRepository<TEntity> baseRepository, IDistributedCache cache) : base(baseRepository, cache)
		{
		}


		protected override void AddOrUpdateEntityInCache(TEntity entity)
		{
			base.AddOrUpdateEntityInCache(entity);

			var entityId = entity.Id;
			var lastUpdatedAtCacheKey = CacheKey.ForEntityLastUpdatedAt<TEntity>(entityId);
			
			_cache.Set(lastUpdatedAtCacheKey, entity.LastUpdatedAt.Ticks.ToString());
		}


		protected override void AddOrUpdateEntitiesInCache(IEnumerable<TEntity> entities)
		{
			base.AddOrUpdateEntitiesInCache(entities);

			var lastUpdatedAtTuples = new List<Tuple<string, string>>();

			foreach (var entity in entities)
			{
				var lastUpdatedAtCacheKey = CacheKey.ForEntityLastUpdatedAt<TEntity>(entity.Id);

				var lastUpdatedAtTuple = new Tuple<string, string>(lastUpdatedAtCacheKey, entity.LastUpdatedAt.Ticks.ToString());
				lastUpdatedAtTuples.Add(lastUpdatedAtTuple);
			}

			_cache.Set(lastUpdatedAtTuples);
		}


		protected override void DeleteEntityFromCache(int entityId)
		{
			base.DeleteEntityFromCache(entityId);

			var lastUpdatedAtCacheKey = CacheKey.ForEntityLastUpdatedAt<TEntity>(entityId);

			_cache.TryRemove(lastUpdatedAtCacheKey);
		}



		protected override void DeleteEntitiesFromCache(IEnumerable<int> entityIds)
		{
			var lastUpdatedAtCachedKeys = new List<string>();

			foreach (var entityId in entityIds)
			{
				var lastUpdatedAtCacheKey = CacheKey.ForEntityLastUpdatedAt<TEntity>(entityId);

				lastUpdatedAtCachedKeys.Add(lastUpdatedAtCacheKey);
			}

			_cache.Remove(lastUpdatedAtCachedKeys);
		}




		public override DateTime GetEntityLastUpdatedAt(int entityId)
		{
			var lastUpdatedAtCacheKey = CacheKey.ForEntityLastUpdatedAt<TEntity>(entityId);

			string lastUpdatedAtTicks = null;
			bool existsInCache = _cache.TryGet(lastUpdatedAtCacheKey, ref lastUpdatedAtTicks);

			if (existsInCache)
			{
				DateTime l3LastUpdatedAt = new DateTime(long.Parse(lastUpdatedAtTicks));
				return l3LastUpdatedAt;
			}

			return (base.GetEntityLastUpdatedAt(entityId));
		}

	}
	
}
