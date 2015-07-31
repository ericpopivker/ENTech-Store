using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.StoreModule
{
	public class ProductCategoryDbEntity : IDbEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public StoreDbEntity Store { get; set; }
		public ICollection<ProductDbEntity> Products { get; set; }
	}
}