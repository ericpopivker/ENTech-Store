using ENTech.Store.Database.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.OrderModule
{
	public class OrderShippingDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Instructions { get; set; }

		public int AddressId { get; set; }
		public AddressDbEntity Address { get; set; }

		public OrderShippingStatus Status { get; set; }
		
		public OrderDbEntity Order { get; set; }
	}
}