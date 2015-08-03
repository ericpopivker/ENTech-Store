using System;
using System.Collections.Generic;
using ENTech.Store.Database.Entities.OrderModule;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Database.Entities.StoreModule
{
	public class ProductDbEntity : IDbEntity, IAuditable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public DateTime? DeletedAt { get; set; }

		public bool IsDeleted { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Sku { get; set; }

		public decimal Price { get; set; }

		public decimal TaxAmount { get; set; }

		public string Photo { get; set; }

		public bool IsActive { get; set; }

		public int StoreId { get; set; }

		public StoreDbEntity Store { get; set; }

		public ICollection<OrderItemDbEntity> OrderItems { get; set; }

		public ProductCategoryDbEntity Category { get; set; }
	}
}