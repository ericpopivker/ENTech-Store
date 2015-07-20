using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.OrderModule.Configurations
{
	internal sealed class OrderConfiguration : EntityTypeConfiguration<OrderDbEntity>
	{
		public OrderConfiguration()
		{
			ToTable("Order");

			Property(x => x.Total).IsRequired();

			HasRequired(x => x.Customer)
				.WithMany(y => y.Orders)
				.HasForeignKey(y => y.CustomerId);

			HasOptional(x => x.Payment)
				.WithRequired()
				.Map(x => x.MapKey("OrderId"));

			HasOptional(x => x.Shipping)
				.WithRequired(x => x.Order)
				.Map(y => y.MapKey("OrderId"));

			HasRequired(x => x.Store)
				.WithMany(y => y.Orders)
				.HasForeignKey(y => y.StoreId);

			HasMany(y => y.Items)
				.WithRequired(y => y.Order)
				.HasForeignKey(y => y.OrderId);
		}
	}
}