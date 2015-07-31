using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.Entities.GeoModule.Configurations
{
	internal sealed class CountryConfiguration : EntityTypeConfiguration<CountryDbEntity>
	{
		public CountryConfiguration()
		{
			Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Code)
				.IsRequired()
				.HasMaxLength(2);
		}
	}
}