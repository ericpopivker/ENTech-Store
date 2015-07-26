using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	public class OrderItem : IDomainEntity
	{
		public int Id { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal SubTotal { get; set; }

		public int ProductId { get; set; }
}
}