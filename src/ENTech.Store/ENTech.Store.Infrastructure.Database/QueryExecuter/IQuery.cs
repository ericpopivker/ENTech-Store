using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Database.QueryExecuter
{
	public interface IQuery
	{
		IEnumerable<int> Find<TCriteria>(TCriteria criteria) where TCriteria : FindCriteriaBase;
	}
}