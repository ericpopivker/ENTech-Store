using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.OrderModule
{
	public class OrderPaymentDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderDbEntity Order { get; set; }
	}
}