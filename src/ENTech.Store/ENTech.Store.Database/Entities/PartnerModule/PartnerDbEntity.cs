using System;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.PartnerModule
{
	public class PartnerDbEntity : IDbEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Key { get; set; }

		public string Secret { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }
	}
}
