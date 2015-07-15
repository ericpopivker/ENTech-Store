using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Services.StoreModule.Projections
{
	public class StoreDbEntity : IDbEntity
	{
		public int Id { get; set; }
	}
}
