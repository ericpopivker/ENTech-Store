using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Entities.CustomerModule;
using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	public enum OrderStatus
	{
		Created = 1,
		Submitted,
		Paid,
		[StringValue("Payment Failed")]
		PaymentFailed,
		Refunded
	}

	[Table("Order")]
	public class Order:IEntity, IAuditable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		[Required]
		public decimal Total { get; set; }

		[Required]
		public int? StoreId { get; set; }

		[ForeignKey("StoreId")]
		public virtual StoreModule.Store Store { get; set; }


		[Required]
		public int? CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }


		public int? PaymentId { get; set; }

		[ForeignKey("PaymentId")]
		public virtual OrderPayment Payment { get; set; }


		public int? ShippingId { get; set; }

		[ForeignKey("ShippingId")]
		public virtual OrderShipping Shipping { get; set; }


		[Column("StatusId")]
		public OrderStatus Status{ get; set; }


		
		public virtual List<OrderItem> Items { get; set; }
	}
}