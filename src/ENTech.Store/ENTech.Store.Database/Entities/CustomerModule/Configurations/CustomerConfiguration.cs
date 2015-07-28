using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.CustomerModule.Configurations
{
	internal sealed class CustomerConfiguration : EntityTypeConfiguration<CustomerDbEntity>
	{
		public CustomerConfiguration()
		{
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
				.HasForeignKey(x => x.StoreId)
				.WillCascadeOnDelete(false);

			HasOptional(x => x.BillingAddress)
				.WithOptionalDependent()
				.WillCascadeOnDelete(false);

			HasOptional(x => x.ShippingAddress)
				.WithOptionalDependent()
				.WillCascadeOnDelete(false);

			HasMany(y => y.Orders)
				.WithRequired(y => y.Customer)
				.HasForeignKey(y => y.CustomerId)
				.WillCascadeOnDelete(false);
		}
	}
}