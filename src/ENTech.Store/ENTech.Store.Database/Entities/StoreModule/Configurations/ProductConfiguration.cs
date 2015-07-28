using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.StoreModule.Configurations
{
	internal sealed class ProductConfiguration : EntityTypeConfiguration<ProductDbEntity>
	{
		public ProductConfiguration()
		{
			Property(x => x.Name)
				.IsRequired();

			Property(x => x.Sku)
				.IsRequired()
				.HasMaxLength(20);

			Property(x => x.Price)
				.IsRequired();

			Property(x => x.StoreId)
				.IsRequired();

			HasRequired(x => x.Store)
				.WithMany(y => y.Products)
				.HasForeignKey(y => y.StoreId)
				.WillCascadeOnDelete(false);

			HasMany(y => y.OrderItems)
				.WithRequired(y => y.Product)
				.HasForeignKey(y => y.ProductId)
				.WillCascadeOnDelete(false);
		}
	}
}