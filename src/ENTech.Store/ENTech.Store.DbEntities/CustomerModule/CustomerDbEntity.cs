using System;
using System.Collections.Generic;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.OrderModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.CustomerModule
{
	public class CustomerDbEntity : IProjection
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

		public StoreDbEntity Store { get; set; }

		public AddressDbEntity BillingAddress { get; set; }

		public AddressDbEntity ShippingAddress { get; set; }

		public IEnumerable<OrderDbEntity> Orders { get; set; }
	}
}