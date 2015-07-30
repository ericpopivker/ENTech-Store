using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public class CachedRepository<TEntity> : IRepository<TEntity> where TEntity : IDomainEntity, IAuditable
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

		public void Add(IEnumerable<TEntity> entity)
		{
			throw new NotImplementedException();
		}


		public void Update(TEntity entity)
		{
			_dbRepository.Update(entity);

			InvalidateEntityInCache(entity);
		}

		public void Update(IEnumerable<TEntity> entities)
		{
			_dbRepository.Update(entities);

			InvalidateEntityInCache(entities);
		}


		public void Delete(IEnumerable<TEntity> entities)
		{
			throw new NotImplementedException();
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

		public IEnumerable<TEntity> FindByIds(IEnumerable<int> entityIds)
		{
			throw new NotImplementedException();
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

		public void Delete(TEntity entity)
		{
			_dbRepository.Delete(entity);

			InvalidateEntityInCache(entity.Id);
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
