using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.GeoModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.GeoModule
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