using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Projections.OrderModule
{
	public class OrderPaymentProjection : IProjection
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }
	}
}