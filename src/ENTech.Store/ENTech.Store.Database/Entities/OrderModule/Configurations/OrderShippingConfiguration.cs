using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.OrderModule.Configurations
{
	internal sealed class OrderShippingConfiguration : EntityTypeConfiguration<OrderShippingDbEntity>
	{
		public OrderShippingConfiguration()
		{
			HasOptional(x => x.Address)
				.WithOptionalPrincipal(x => x.Shipping)
				.WillCascadeOnDelete(true);

			HasOptional(x => x.Order)
				.WithOptionalDependent(y => y.Shipping)
				.WillCascadeOnDelete(false);
		}
	}
}