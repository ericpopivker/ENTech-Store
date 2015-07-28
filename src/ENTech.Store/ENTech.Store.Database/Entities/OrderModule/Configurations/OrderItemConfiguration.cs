using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.OrderModule.Configurations
{
	internal sealed class OrderItemConfiguration : EntityTypeConfiguration<OrderItemDbEntity>
	{
		public OrderItemConfiguration()
		{
			Property(x => x.Quantity)
				.IsRequired();

			Property(x => x.SubTotal)
				.IsRequired();

			Property(x => x.UnitPrice)
				.IsRequired();

			HasRequired(x => x.Order)
				.WithMany(y => y.Items)
				.HasForeignKey(x => x.OrderId)
				.WillCascadeOnDelete(false);

			HasRequired(x => x.Product)
				.WithMany(y => y.OrderItems)
				.HasForeignKey(y => y.ProductId)
				.WillCascadeOnDelete(false);
		}
	}
}