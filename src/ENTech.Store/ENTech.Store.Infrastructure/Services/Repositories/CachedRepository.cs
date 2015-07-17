using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Services.Repositories
{
	public class CachedRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity, IAuditable
	{
		private IRepository<TEntity> _dbRepository;
		private ICache _l2Cache;
		private ICache _l3Cache; 

		public CachedRepository(IRepository<TEntity> dbRepository, ICache l2Cache, ICache l3Cache)
		{
			_dbRepository = dbRepository;
			_l2Cache = l2Cache;
			_l3Cache = l3Cache;
		} 


		public void Add(TEntity entity)
		{
			_dbRepository.Add(entity);
		}

		public void Update(TEntity entity)
		{
			_dbRepository.Update(entity);

			InvalidateEntityInCache(entity.Id);
		}

		public TEntity GetById(int entityId)
		{
			var entityCacheKey = GetEntityCacheKey(entityId);
			var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);

			DateTime? l3LastUpdatedAt = null;
			bool existsInL3Cache = _l3Cache.TryGet(lastUpdatedAtCacheKey, ref l3LastUpdatedAt);

			TEntity entity;

			if (existsInL3Cache)
			{
				DateTime? l2LastUpdatedAt = null;
				bool existsInL2Cache = _l2Cache.TryGet(lastUpdatedAtCacheKey, ref l2LastUpdatedAt);

				if (existsInL2Cache)
				{
					if (l2LastUpdatedAt != l3LastUpdatedAt)
					{
						entity = _l3Cache.Get<TEntity>(entityCacheKey);

						_l2Cache.Set(entityCacheKey, entity);
						_l2Cache.Set(lastUpdatedAtCacheKey, l3LastUpdatedAt);
					}
					else
					{
						entity = _l2Cache.Get<TEntity>(entityCacheKey);
					}
				}
				else
				{
					entity = _l3Cache.Get<TEntity>(entityCacheKey);

					_l2Cache.Set(entityCacheKey, entity);
					_l2Cache.Set(lastUpdatedAtCacheKey, l3LastUpdatedAt);
				}
			}
			else
			{
				entity = _dbRepository.GetById(entityId);
		
				_l3Cache.Set(entityCacheKey, entity);
				_l3Cache.Set(lastUpdatedAtCacheKey, entity.LastUpdatedAt);

				_l2Cache.Set(entityCacheKey, entity);
				_l2Cache.Set(lastUpdatedAtCacheKey, entity.LastUpdatedAt);
			}

			
			return entity;
		}


		public string GetLastUpdateAtCacheKey(int entityId)
		{
			string key = String.Format("{0}_LastUpdatedAt", GetEntityCacheKey(entityId));
			return key;
		}


		public string GetEntityCacheKey(int entityId)
		{
			string key = String.Format("{0}_{1}", typeof (TEntity).FullName, entityId);
			return key;
		}



		public TEntity FindByIds(IList<int> entityIds)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int entityId)
		{
			_dbRepository.Delete(entityId);

			InvalidateEntityInCache(entityId);
		}

		public EntityMetaState GetEntityMetaState(int entityId)
		{
			return _dbRepository.GetEntityMetaState(entityId);
		}

		public void InvalidateEntityInCache(int entityId)
		{
			var entityCacheKey = GetEntityCacheKey(entityId);
			var lastUpdatedAtCacheKey = GetLastUpdateAtCacheKey(entityId);
			
			_l3Cache.TryRemove(entityCacheKey);
			_l3Cache.TryRemove(lastUpdatedAtCacheKey);

			_l2Cache.TryRemove(entityCacheKey);
			_l2Cache.TryRemove(lastUpdatedAtCacheKey);
		}
	}
}
