using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.StoreModule.Configurations
{
	internal sealed class StoreConfiguration : EntityTypeConfiguration<StoreDbEntity>
	{
		public StoreConfiguration()
		{
			ToTable("Store");

			Property(x => x.Email).HasMaxLength(255);

			Property(x => x.Phone).HasMaxLength(20);

			Property(x => x.TimezoneId).IsRequired();

			HasMany(x => x.Products)
				.WithRequired(y=>y.Store)
				.HasForeignKey(y=>y.StoreId);

			HasRequired(o => o.Address)
				.WithOptional()
				.Map(o => o.MapKey("AddressId"));
		}
	}
}