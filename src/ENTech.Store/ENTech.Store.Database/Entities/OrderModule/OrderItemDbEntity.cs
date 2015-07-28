using ENTech.Store.Database.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.OrderModule
{
	public class OrderItemDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal SubTotal { get; set; }

		public int OrderId { get; set; }
		public OrderDbEntity Order { get; set; }

		public int ProductId { get; set; }
		public ProductDbEntity Product { get; set; }
	}
}