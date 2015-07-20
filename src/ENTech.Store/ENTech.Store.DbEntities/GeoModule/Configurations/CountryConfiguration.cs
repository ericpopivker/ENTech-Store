using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.DbEntities.GeoModule.Configurations
{
	internal sealed class CountryConfiguration : EntityTypeConfiguration<CountryDbEntity>
	{
		public CountryConfiguration()
		{
			ToTable("Country");

			Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Code)
				.IsRequired()
				.HasMaxLength(2);
		}
	}
}