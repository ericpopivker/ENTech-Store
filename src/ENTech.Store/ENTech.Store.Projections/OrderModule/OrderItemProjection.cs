using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.StoreModule;

namespace ENTech.Store.Projections.OrderModule
{
	public class OrderItemProjection : IProjection
	{
		public int Id { get; set; }

		public int? Quantity { get; set; }

		public decimal? UnitPrice { get; set; }

		public decimal? SubTotal { get; set; }
		
		public OrderProjection Order { get; set; }

		public ProductProjection Product { get; set; }
	}
}