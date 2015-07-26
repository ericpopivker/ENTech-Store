using System;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.CustomerModule
{
	public class Customer : IDomainEntity, IAuditable, ILogicallyDeletable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedAt { get; set; }

		public string FirstName { get; set; }
		
		public string LastName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }
		
		public int? BillingAddressId { get; set; }
		
		public int? ShippingAddressId { get; set; }
	}
}