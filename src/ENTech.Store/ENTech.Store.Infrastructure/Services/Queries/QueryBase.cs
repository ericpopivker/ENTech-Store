using System.Linq;
using ENTech.Store.Infrastructure.EnityFramework;
using ENTech.Store.Infrastructure.Enums;
using ENTech.Store.Infrastructure.Extensions;

namespace ENTech.Store.Infrastructure.Services.Queries
{
	public abstract class QueryBase<TDbContext, TCriteria, TEntity> 
		where TCriteria : CriteriaBase
		where TDbContext : IDbContext
	{
		private readonly TDbContext _dbContext;

		private IQueryable<TEntity> _query;

		private TCriteria _criteria;

		public QueryBase(TDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public TDbContext DbContext { get { return _dbContext; } }

		public TCriteria Criteria
		{
			get { return _criteria; }
			set
			{
				//reset query
				_query = GetQuery();

				//store value
				_criteria = value;

				//apply criteria
				ApplyCustomFilters(ref _query);
			}
		}

		public int GetTotalCount()
		{
			return _query.Count();
		}

		public int GetPageCount()
		{
			return GetTotalCount() / Criteria.PageSize;
		}

		public IQueryable<TEntity> Execute()
		{
			var sortedQuery = ApplySorting(_query);
			return sortedQuery.GetPage(_criteria.PageIndex, _criteria.PageSize);
		}

		protected abstract IQueryable<TEntity> GetQuery();

		protected abstract void ApplyCustomFilters(ref IQueryable<TEntity> query);

		protected IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query)
		{
			ApplySortCriteria(ref query, Criteria.SortCriteria1);
			ApplySortCriteria(ref query, Criteria.SortCriteria2);
			return query;
		}

		protected void ApplySortCriteria(ref IQueryable<TEntity> query, SortCriteria sortCriteria)
		{
			if (sortCriteria != null)
			{
				if (sortCriteria.Direction == SortDirection.Ascending)
					query = query.OrderBy(sortCriteria.Field);
				else
					query = query.OrderByDescending(sortCriteria.Field);
			}
		}
	}
}
