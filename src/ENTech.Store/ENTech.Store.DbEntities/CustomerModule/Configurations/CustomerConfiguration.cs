using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.CustomerModule.Configurations
{
	internal sealed class CustomerConfiguration : EntityTypeConfiguration<CustomerDbEntity>
	{
		public CustomerConfiguration()
		{
			ToTable("Customer");

			Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(255);

			Property(x => x.Phone)
				.HasMaxLength(20);

			HasRequired(x => x.Store)
				.WithMany(x=>x.Customers)
				.HasForeignKey(x=>x.StoreId);

			HasOptional(x => x.BillingAddress)
				.WithOptionalDependent()
				.Map(y=>y.MapKey("BillingAddressId"));

			HasOptional(x => x.ShippingAddress)
				.WithOptionalDependent()
				.Map(y => y.MapKey("ShippingAddressId"));

			HasMany(y => y.Orders)
				.WithRequired(y => y.Customer)
				.HasForeignKey(y => y.CustomerId);
		}
	}
}