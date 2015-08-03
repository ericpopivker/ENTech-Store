using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Database.Entities.StoreModule
{
	public class ProductCategoryDbEntity : IDbEntity, IAuditable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public StoreDbEntity Store { get; set; }
		public ICollection<ProductDbEntity> Products { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }
	}
}