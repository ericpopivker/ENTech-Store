using ENTech.Store.Database.Entities.OrderModule;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.GeoModule
{
	public class AddressDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Street2 { get; set; }

		public StateDbEntity State { get; set; }

		public string StateOther { get; set; }

		public CountryDbEntity Country { get; set; }
		
		public StoreDbEntity Store { get; set; }
		
		public OrderShippingDbEntity Shipping { get; set; }
	}
}
