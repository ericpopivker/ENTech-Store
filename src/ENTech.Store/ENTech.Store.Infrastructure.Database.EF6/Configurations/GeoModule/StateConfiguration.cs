using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.GeoModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.GeoModule
{
	internal sealed class StateConfiguration : EntityTypeConfiguration<StateDbEntity>
	{
		public StateConfiguration()
		{
			Property(x => x.Name)
				.HasMaxLength(100);

			Property(x => x.Code)
				.IsRequired()
				.HasMaxLength(2);
		}
	}
}