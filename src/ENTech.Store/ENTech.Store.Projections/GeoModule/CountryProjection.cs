using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Projections.GeoModule
{
	public class CountryProjection : IProjection
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
	}
}
