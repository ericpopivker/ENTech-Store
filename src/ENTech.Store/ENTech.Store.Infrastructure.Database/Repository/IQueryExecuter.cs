namespace ENTech.Store.Infrastructure.Database.Repository
{
	public interface IQueryExecuter
	{
		T Execute<T>(QueryCriteria<T> criteria) where T : IProjection;
	}
}