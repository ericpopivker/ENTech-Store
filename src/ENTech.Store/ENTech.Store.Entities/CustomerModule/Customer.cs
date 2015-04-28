using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.CustomerModule
{
	[Table("Customer")]
	public class Customer : IEntity, IAuditable, ILogicallyDeletable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedAt { get; set; }


		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(255)]
		public string Email { get; set; }

		[MaxLength(20)]
		public string Phone { get; set; }

		[Required]
		public int? StoreId { get; set; }

		[ForeignKey("StoreId")]
		public virtual StoreModule.Store Store { get; set; }
		
		public int? BillingAddressId { get; set; }
		
		[ForeignKey("BillingAddressId")]
		public virtual Address BillingAddress { get; set; }
		
		public int? ShippingAddressId { get; set; }
		
		[ForeignKey("ShippingAddressId")]
		public virtual Address ShippingAddress { get; set; } 

	
	}
}