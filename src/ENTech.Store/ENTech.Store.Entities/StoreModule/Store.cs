using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.StoreModule
{
	[Table("Store")]
	public class Store : IEntity, IAuditable, ILogicallyDeletable
	{
		public int Id { get; set; }
		
		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedAt { get; set; }
		
		public string Name { get; set; }

		public string Logo { get; set; }
		
		[MaxLength(20)]
		public string Phone { get; set; }

		[MaxLength(255)]
		public string Email { get; set; }

		[Required]
		public string TimezoneId { get; set; }

		public int[] ProductIds { get; set; }

		public int? AddressId { get; set; }
	}
}
