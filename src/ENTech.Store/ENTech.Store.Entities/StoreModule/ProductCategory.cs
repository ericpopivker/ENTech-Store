using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.StoreModule
{
	public class ProductCategory : IDomainEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}