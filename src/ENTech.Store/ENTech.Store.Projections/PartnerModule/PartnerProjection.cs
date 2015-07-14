using System;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Projections.PartnerModule
{
	public class PartnerProjection : IProjection
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Key { get; set; }

		public string Secret { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }
	}
}
