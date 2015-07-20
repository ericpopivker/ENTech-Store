using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.OrderModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.OrderModule
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