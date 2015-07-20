using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.GeoModule.Configurations
{
	internal sealed class AddressConfiguration : EntityTypeConfiguration<AddressDbEntity>
	{
		public AddressConfiguration()
		{
			ToTable("Address");

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
				.WithOptionalDependent();

			HasOptional(x => x.Country)
				.WithOptionalDependent();
		}
	}
}