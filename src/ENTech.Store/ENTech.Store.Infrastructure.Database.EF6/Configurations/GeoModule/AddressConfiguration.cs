using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.GeoModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.GeoModule
{
	internal sealed class AddressConfiguration : EntityTypeConfiguration<AddressDbEntity>
	{
		public AddressConfiguration()
		{
			Property(x => x.Street)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Street2)
				.HasMaxLength(100);

			Property(x => x.City)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Zip)
				.HasMaxLength(20);

			HasOptional(x => x.State)
				.WithOptionalDependent()
				.WillCascadeOnDelete(false);

			HasOptional(x => x.Country)
				.WithOptionalDependent()
				.WillCascadeOnDelete(false);

			HasOptional(x => x.Store)
				.WithOptionalDependent(x => x.Address)
				.WillCascadeOnDelete(false);
		}
	}
}