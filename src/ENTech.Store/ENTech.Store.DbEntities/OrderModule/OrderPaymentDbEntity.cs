using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.OrderModule
{
	public class OrderPaymentDbEntity : IProjection
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }
	}
}