namespace ENTech.Store.Infrastructure.Services.UnitOfWork
{
	public interface IUnitOfWork<out TDbContext> 
	{
		TDbContext DbContext { get; }

		void Add<TEntity>(TEntity entity) where TEntity : class;

		void Update<TEntity>(TEntity entity) where TEntity : class;

		void Remove<TEntity>(TEntity entity) where TEntity : class;


		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();


		void Dispose();

		bool IsDisposed { get; }

		void SaveChanges();
	}
}