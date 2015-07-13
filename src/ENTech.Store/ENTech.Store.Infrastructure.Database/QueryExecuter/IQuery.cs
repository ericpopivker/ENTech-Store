using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Database.QueryExecuter
{
	public interface IQuery<out TProjection> where TProjection : IProjection
	{
		TProjection GetById(int id);
		IEnumerable<TProjection> Find<TCriteria>(TCriteria criteria) where TCriteria : FindCriteriaBase;
	}
}