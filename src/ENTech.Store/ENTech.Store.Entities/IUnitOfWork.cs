namespace ENTech.Store.Entities
{
	public interface IUnitOfWork 
	{
		IDbContext DbContext { get; }

		void Add<TEntity>(TEntity entity) where TEntity : class;

		void Update<TEntity>(TEntity entity) where TEntity : class;

		void Remove<TEntity>(TEntity entity) where TEntity : class;
		
		TResult Query<TResult, TCriteria>(IQuery<TCriteria, TResult> query, TCriteria criteria)
			where TCriteria : IQueryCriteria;

		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();


		void Dispose();

		bool IsDisposed { get; }

		void SaveChanges();
	}
}
