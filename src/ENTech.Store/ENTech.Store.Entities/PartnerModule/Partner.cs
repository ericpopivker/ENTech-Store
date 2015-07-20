using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.PartnerModule
{
	[Table("Partner")]
	public class Partner : IDomainEntity, IAuditable
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Key { get; set; }

		[Required]
		public string Secret { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }
	}
}
