using System;
using System.Collections.Generic;
using ENTech.Store.Database.Entities.CustomerModule;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.OrderModule
{
	public class OrderDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public decimal Total { get; set; }

		public int StoreId { get; set; }
		public StoreDbEntity Store { get; set; }

		public int CustomerId { get; set; }
		public CustomerDbEntity Customer { get; set; }

		public OrderPaymentDbEntity Payment { get; set; }

		public OrderShippingDbEntity Shipping { get; set; }

		public OrderStatus Status{ get; set; }

		public List<OrderItemDbEntity> Items { get; set; }
		
	}
}