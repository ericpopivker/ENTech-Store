using System.Collections.Generic;
using System.Data.Entity;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Database
{
	public abstract class QueryBase<TEntity> : IQuery where TEntity : class, IDbEntity
	{
		private readonly IDbSet<TEntity> _dbSet;

		protected QueryBase(IDbContext dbContext)
		{
			_dbSet = dbContext.GetDbSet<TEntity>();
		}

		protected IDbSet<TEntity> DbSet { get { return _dbSet; } }

		public abstract IEnumerable<int> Find<TCriteria>(TCriteria criteria) where TCriteria : FindCriteriaBase;
	}
}