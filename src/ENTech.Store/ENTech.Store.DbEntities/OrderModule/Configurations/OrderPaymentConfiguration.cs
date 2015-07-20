using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.OrderModule.Configurations
{
	internal sealed class OrderPaymentConfiguration : EntityTypeConfiguration<OrderPaymentDbEntity>
	{
		public OrderPaymentConfiguration()
		{
			ToTable("OrderPayment");
		}
	}
}