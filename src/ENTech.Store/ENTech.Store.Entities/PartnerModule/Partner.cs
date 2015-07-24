using System;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.PartnerModule
{
	public class Partner : IDomainEntity, IAuditable
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Key { get; set; }

		public string Secret { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }
	}
}
