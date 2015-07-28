using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.GeoModule
{
	public class StateDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
	}
}
