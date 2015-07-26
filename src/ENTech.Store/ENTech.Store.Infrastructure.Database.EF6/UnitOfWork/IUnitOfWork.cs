using System;

namespace ENTech.Store.Infrastructure.Database.EF6.UnitOfWork
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
