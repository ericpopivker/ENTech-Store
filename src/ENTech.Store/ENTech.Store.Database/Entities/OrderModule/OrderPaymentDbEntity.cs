using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.OrderModule
{
	public class OrderPaymentDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderDbEntity Order { get; set; }
	}
}