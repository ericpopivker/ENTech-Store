using System;
using System.Collections.Generic;
using System.Data.Entity;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class QueryBase<TEntity> : IQuery where TEntity : class, IDbEntity
	{
		private readonly IDbSet<TEntity> _dbSet;

		public QueryBase(IDbSet<TEntity> dbSet)
		{
			_dbSet = dbSet;
		}
		
		public IEnumerable<int> Find<TCriteria>(TCriteria criteria) where TCriteria : FindCriteriaBase
		{
			throw new NotImplementedException();
		}
	}
}