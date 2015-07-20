using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.OrderModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.OrderModule
{
	internal sealed class OrderConfiguration : EntityTypeConfiguration<OrderDbEntity>
	{
		public OrderConfiguration()
		{
			Property(x => x.Total).IsRequired();

			HasRequired(x => x.Customer)
				.WithMany(y => y.Orders)
				.HasForeignKey(y => y.CustomerId)
				.WillCascadeOnDelete(false);

			HasOptional(x => x.Payment)
				.WithOptionalPrincipal(x => x.Order)
				.WillCascadeOnDelete(false);

			HasOptional(x => x.Shipping)
				.WithOptionalPrincipal(x => x.Order)
				.WillCascadeOnDelete(false);

			HasRequired(x => x.Store)
				.WithMany(y => y.Orders)
				.HasForeignKey(y => y.StoreId)
				.WillCascadeOnDelete(false);

			HasMany(y => y.Items)
				.WithRequired(y => y.Order)
				.HasForeignKey(y => y.OrderId)
				.WillCascadeOnDelete(false);
		}
	}
}