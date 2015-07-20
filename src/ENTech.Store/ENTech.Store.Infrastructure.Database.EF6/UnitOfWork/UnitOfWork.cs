using System;

namespace ENTech.Store.Infrastructure.Database.EF6.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private DbContextScope _scope;

		public UnitOfWork()
		{
			var lazyScope = new Lazy<DbContextScope>(() =>
			{
				return new DbContextScope();
			});

			_scope = lazyScope.Value;
		}

		public IDbContext DbContext
		{
			get
			{
				if (IsDisposed) throw new ObjectDisposedException("This UnitOfWork instanse is disposed");

				return DbContextScope.CurrentDbContext;
			}
		}

		public void BeginTransaction()
		{
			DbContext.BeginTransaction();
		}

		public void CompleteTransaction()
		{
			DbContext.CompleteTransaction();
		}

		public void RollbackTransaction()
		{
			DbContext.RollbackTransaction();
		}

		public void Dispose()
		{
			if (_scope != null)
			{
				_scope.Dispose();
			}
			IsDisposed = true;
		}

		public bool IsDisposed { get; private set; }

		public void SaveChanges()
		{
			DbContext.SaveChanges();
		}
	}
}
