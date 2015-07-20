using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.OrderModule.Configurations
{
	internal sealed class OrderShippingConfiguration : EntityTypeConfiguration<OrderShippingDbEntity>
	{
		public OrderShippingConfiguration()
		{
			ToTable("OrderShipping");

			HasRequired(x => x.Address)
				.WithOptional()
				.Map(x => x.MapKey("AddressId"));
		}
	}
}