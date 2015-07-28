using System;

namespace ENTech.Store.Infrastructure.Database.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();

		bool IsDisposed { get; }
		void SaveChanges();
	}
}
