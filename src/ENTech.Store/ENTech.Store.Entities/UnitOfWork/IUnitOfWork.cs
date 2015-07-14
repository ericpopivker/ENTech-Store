using System;

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
	}
}
