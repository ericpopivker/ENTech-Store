using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.Entities.StoreModule.Configurations
{
	public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategoryDbEntity>
	{
		public ProductCategoryConfiguration()
		{
			Property(x => x.Name)
				.HasMaxLength(50)
				.IsRequired();

			HasMany(x => x.Products)
				.WithRequired(y => y.Category)
				.WillCascadeOnDelete(false);

			HasRequired(x=>x.Store)
				.WithMany(y=>y.Categories)
				.WillCascadeOnDelete(false);
		}
	}
}