using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.GeoModule
{
	public class State : IDomainEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
	}
}
