using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Services.StoreModule.Projections;

namespace ENTech.Store.Services.StoreModule
{
	public interface IStoreQueryExecuter : IQueryExecuter<StoreProjection>
	{
	}
}