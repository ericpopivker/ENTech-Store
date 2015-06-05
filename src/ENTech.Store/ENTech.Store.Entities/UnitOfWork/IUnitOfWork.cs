using System;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IDbContext DbContext { get; }

		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();

		bool IsDisposed { get; }
		void SaveChanges();

		T Add<T>(T entity) where T : class, IEntity;

		void Delete<T>(T entity) where T : class, IEntity;

		void LogicallyDelete<T>(T entity) where T : class, ILogicallyDeletable;

		TResult Query<TResult, TCriteria>(IQuery<TCriteria, TResult> query, TCriteria criteria)
			where TCriteria : IQueryCriteria;
	}
}
