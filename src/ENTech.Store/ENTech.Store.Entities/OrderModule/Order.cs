using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	public class Order : IDomainEntity, IAuditable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public decimal Total { get; set; }

		public int StoreId { get; set; }
		
		public int? CustomerId { get; set; }

		public virtual OrderPayment Payment { get; set; }

		public virtual OrderShipping Shipping { get; set; }

		public OrderStatus Status{ get; set; }

		public IEnumerable<OrderItem> Items { get; set; }
	}
}