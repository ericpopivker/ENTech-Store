using System;
using System.Collections.Generic;
using ENTech.Store.DbEntities.CustomerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.OrderModule
{
	public class OrderDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public decimal Total { get; set; }
		
		public StoreDbEntity Store { get; set; }
		
		public CustomerDbEntity Customer { get; set; }

		public OrderPaymentDbEntity Payment { get; set; }

		public OrderShippingDbEntity Shipping { get; set; }

		public OrderStatus Status{ get; set; }

		public List<OrderItemDbEntity> Items { get; set; }
	}
}