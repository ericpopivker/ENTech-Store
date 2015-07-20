using System.Collections.Generic;
using System.Data.Entity;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public abstract class QueryBase<TEntity> : IQuery where TEntity : class, IDbEntity
	{
		private readonly IDbSet<TEntity> _dbSet;

		protected QueryBase(IDbSet<TEntity> dbSet)
		{
			_dbSet = dbSet;
		}

		protected IDbSet<TEntity> DbSet { get { return _dbSet; } }

		public abstract IEnumerable<int> Find<TCriteria>(TCriteria criteria) where TCriteria : FindCriteriaBase;
	}
}