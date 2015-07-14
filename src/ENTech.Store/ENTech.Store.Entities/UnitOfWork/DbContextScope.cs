using System;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Context;

namespace ENTech.Store.Entities.UnitOfWork
{
	public class DbContextScope : IDisposable
	{
		private const string OpenedCountPropertyName = "DbContextScope.OpenedCount";
		private const string DbContextPropertyName = "DbContextScope.CurrentDbContext";

		private static readonly IContextStorage ContextStorage = new Lazy<IContextStorage>(() => new CallContextStorage()).Value;
		private bool _isDbContextRegistrator;

		private static int OpenedCount
		{
			get
			{
				if (!ContextStorage.Contains(OpenedCountPropertyName)) return 0;
				return ContextStorage.Get<int>(OpenedCountPropertyName);
			}

			set { ContextStorage.Store(OpenedCountPropertyName, value); }
		}

		public static IDbContext CurrentDbContext
		{
			get
			{
				if (!ContextStorage.Contains(DbContextPropertyName)) return null;
				return ContextStorage.Get<IDbContext>(DbContextPropertyName);
			}

			set { ContextStorage.Store(DbContextPropertyName, value); }
		}

		public DbContextScope()
		{
			if (OpenedCount == 0)
			{
				CurrentDbContext = IoC.Resolve<IDbContext>();

				_isDbContextRegistrator = true;
			}

			OpenedCount++;
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			OpenedCount--;

			if (OpenedCount == 0 || _isDbContextRegistrator)
			{
				CurrentDbContext.Dispose();
				CurrentDbContext = null;
			}
		}

		#endregion
	}
}