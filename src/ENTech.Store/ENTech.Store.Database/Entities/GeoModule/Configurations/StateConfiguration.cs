using System.Data.Entity.ModelConfiguration;

namespace ENTech.Store.Database.GeoModule.Configurations
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