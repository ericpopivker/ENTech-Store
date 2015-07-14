using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Projections.GeoModule
{
	public class AddressProjection : IProjection
	{
		public int Id { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Street2 { get; set; }

		public StateProjection State { get; set; }

		public string StateOther { get; set; }

		public CountryProjection Country { get; set; }
	}
}
