using System;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.PartnerModule
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
