using System;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.OrderModule;

namespace ENTech.Store.Projections.StoreModule
{
	public class ProductProjection : IProjection
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

		public StoreProjection Store { get; set; }

		public OrderItemProjection OrderItems { get; set; }
	}
}