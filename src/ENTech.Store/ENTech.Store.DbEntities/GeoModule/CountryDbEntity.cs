using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.GeoModule
{
	public class CountryDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
	}
}
