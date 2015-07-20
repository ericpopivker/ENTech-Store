using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.OrderModule
{
	public class OrderPaymentDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderDbEntity Order { get; set; }
	}
}