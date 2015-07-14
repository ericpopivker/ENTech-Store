using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.CustomerModule;
using ENTech.Store.Projections.GeoModule;

namespace ENTech.Store.Projections.StoreModule
{
	public class StoreProjection : IProjection
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

		public AddressProjection Address { get; set; }

		public IEnumerable<CustomerProjection> Customers { get; set; }

		public IEnumerable<ProductProjection> Products { get; set; }
	}
}
