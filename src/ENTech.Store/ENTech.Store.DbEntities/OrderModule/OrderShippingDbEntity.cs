using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.OrderModule
{
	public class OrderShippingDbEntity : IProjection
	{
		public int Id { get; set; }

		public string Instructions { get; set; }
		
		public AddressDbEntity Address { get; set; }

		public OrderShippingStatus Status { get; set; }
	}
}