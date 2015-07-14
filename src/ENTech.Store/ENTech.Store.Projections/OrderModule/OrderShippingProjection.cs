using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.GeoModule;

namespace ENTech.Store.Projections.OrderModule
{
	public class OrderShippingProjection : IProjection
	{
		public int Id { get; set; }

		public string Instructions { get; set; }
		
		public AddressProjection Address { get; set; }

		public OrderShippingStatus Status { get; set; }
	}
}