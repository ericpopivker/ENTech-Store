using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.OrderModule.Configurations
{
	internal sealed class OrderPaymentConfiguration : EntityTypeConfiguration<OrderPaymentDbEntity>
	{
		public OrderPaymentConfiguration()
		{
			HasOptional(x => x.Order)
				.WithOptionalDependent(y => y.Payment)
				.WillCascadeOnDelete(false);
		}
	}
}