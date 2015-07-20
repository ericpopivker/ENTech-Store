using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public class DbEntityStateKeeper<TEntity, TDbEntity> : IDbEntityStateKeeper<TEntity, TDbEntity>
		where TEntity : IDomainEntity
		where TDbEntity : IDbEntity
	{
		private IDictionary<TEntity, TDbEntity> _hashStorage = new Dictionary<TEntity, TDbEntity>();

		public void Store(TEntity entity, TDbEntity dbEntity)
		{
			if (_hashStorage.ContainsKey(entity))
				throw new EntityTrackedException();

			_hashStorage.Add(entity, dbEntity);
		}

		public TDbEntity Get(TEntity entity)
		{
			if (_hashStorage.ContainsKey(entity) == false)
				throw new EntityNotTrackedException();

			return _hashStorage[entity];
		}

		public void Remove(TEntity entity)
		{
			if (_hashStorage.ContainsKey(entity) == false)
				throw new EntityNotTrackedException();

			_hashStorage.Remove(entity);
		}

		public void Clear()
		{
			_hashStorage.Clear();
		}
	}
}