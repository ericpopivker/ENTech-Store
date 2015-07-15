using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IDbEntityMapper
	{
		TDbEntity CreateDbEntity<TEntity, TDbEntity>(TEntity source)
			where TDbEntity : IDbEntity
			where TEntity : IEntity;

		void ApplyChanges<TEntity, TDbEntity>(TEntity source, TDbEntity dbEntity)
			where TDbEntity : IDbEntity
			where TEntity : IEntity;

		TEntity MapToEntity<TDbEntity, TEntity>(TDbEntity dbEntity)
			where TDbEntity : IDbEntity
			where TEntity : IEntity;

		IEnumerable<TEntity> MapToEntities<TDbEntity, TEntity>(IEnumerable<TDbEntity> dbEntities)
			where TDbEntity : IDbEntity
			where TEntity : IEntity;
	}
}