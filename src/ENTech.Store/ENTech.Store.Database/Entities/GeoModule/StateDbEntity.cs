using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.GeoModule
{
	public class StateDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
	}
}
