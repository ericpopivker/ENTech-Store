using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Database.Utility
{
	public class DbEntityMapper : IDbEntityMapper
	{
		public TDbEntity CreateDbEntity<TEntity, TDbEntity>(TEntity source) where TEntity : IDomainEntity where TDbEntity : IDbEntity
		{
			return AutoMapper.Mapper.Map<TDbEntity>(source);
		}

		public void ApplyChanges<TEntity, TDbEntity>(TEntity source, TDbEntity dbEntity) where TEntity : IDomainEntity where TDbEntity : IDbEntity
		{
			AutoMapper.Mapper.Map(source, dbEntity);
		}

		public TEntity MapToEntity<TDbEntity, TEntity>(TDbEntity dbEntity) where TDbEntity : IDbEntity where TEntity : IDomainEntity
		{
			return AutoMapper.Mapper.Map<TEntity>(dbEntity);
		}

		public IEnumerable<TEntity> MapToEntities<TDbEntity, TEntity>(IEnumerable<TDbEntity> dbEntities) where TDbEntity : IDbEntity where TEntity : IDomainEntity
		{
			return AutoMapper.Mapper.Map<IEnumerable<TEntity>>(dbEntities);
		}
	}
}