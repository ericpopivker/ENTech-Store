using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Projections.GeoModule;
using ENTech.Store.Projections.OrderModule;
using ENTech.Store.Projections.StoreModule;

namespace ENTech.Store.Projections.CustomerModule
{
	public class CustomerProjection : IProjection
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

		public StoreProjection Store { get; set; }

		public AddressProjection BillingAddress { get; set; }

		public AddressProjection ShippingAddress { get; set; }

		public IEnumerable<OrderProjection> Orders { get; set; }
	}
}