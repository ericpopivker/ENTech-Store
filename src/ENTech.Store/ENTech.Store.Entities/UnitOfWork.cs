using System;
using ENTech.Store.Infrastructure.Utils;

namespace ENTech.Store.Entities
{
	public class UnitOfWork : IUnitOfWork
	{
		private const string CallContextDataKey = "DbContext";
		private IDbContextFactory _dbContextFactory;

		public UnitOfWork(IDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}


		public virtual IDbContext DbContext
		{
			get
			{
				if (IsDisposed) throw new ObjectDisposedException("This UnitOfWork instanse is disposed");

				var dbContext = CallContextUtils.GetData<IDbContext>(CallContextDataKey);
				if (dbContext == null || dbContext.IsDisposed)
				{
					dbContext = _dbContextFactory.Create();
					CallContextUtils.SetData(CallContextDataKey, dbContext);
				}

				return dbContext;
			}
		}

		public void Add<TEntity>(TEntity entity) where TEntity : class
		{
			DbContext.DbSet<TEntity>().Add(entity);
		}

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{

		}

		public void Remove<TEntity>(TEntity entity) where TEntity : class
		{
			DbContext.DbSet<TEntity>().Remove(entity);
		}

		public TResult Query<TResult, TCriteria>(IQuery<TCriteria, TResult> query, TCriteria criteria) 
						where TCriteria : IQueryCriteria
		{
			throw new NotImplementedException();
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
			if (DbContext != null)
			{
				if (!DbContext.IsDisposed)
				{
					DbContext.Dispose();
				}

				CallContextUtils.RemoveData(CallContextDataKey);
			}
			IsDisposed = true;
		}

		public bool IsDisposed { get; private set; }

		public void SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
