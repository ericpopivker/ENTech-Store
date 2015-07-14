using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.CustomerModule;
using ENTech.Store.Projections.StoreModule;

namespace ENTech.Store.Projections.OrderModule
{
	public class OrderProjection : IProjection
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public decimal Total { get; set; }
		
		public StoreProjection Store { get; set; }
		
		public CustomerProjection Customer { get; set; }

		public OrderPaymentProjection Payment { get; set; }

		public OrderShippingProjection Shipping { get; set; }

		public OrderStatus Status{ get; set; }

		public List<OrderItemProjection> Items { get; set; }
	}
}