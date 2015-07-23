using System;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.StoreModule
{
	public class Store : IDomainEntity, IAuditable, ILogicallyDeletable
	{
		public int Id { get; set; }
		
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		
		public string Name { get; set; }
		public string Logo { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string TimezoneId { get; set; }

		public int? AddressId { get; set; }
		public int[] ProductIds { get; set; }
	}
}