using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.StoreModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.StoreModule
{
	internal sealed class StoreConfiguration : EntityTypeConfiguration<StoreDbEntity>
	{
		public StoreConfiguration()
		{
			Property(x => x.Email).HasMaxLength(255);

			Property(x => x.Phone).HasMaxLength(20);

			Property(x => x.TimezoneId).IsRequired();

			HasMany(x => x.Products)
				.WithRequired(y => y.Store)
				.HasForeignKey(y => y.StoreId);

			HasOptional(x => x.Address)
				.WithOptionalPrincipal(x => x.Store);
		}
	}
}