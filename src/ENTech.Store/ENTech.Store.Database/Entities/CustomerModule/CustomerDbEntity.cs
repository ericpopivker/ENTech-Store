using System;
using System.Collections.Generic;
using ENTech.Store.Database.Entities.GeoModule;
using ENTech.Store.Database.Entities.OrderModule;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database.Entities.CustomerModule
{
	public class CustomerDbEntity : IDbEntity
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

		public int StoreId { get; set; }
		public StoreDbEntity Store { get; set; }

		public AddressDbEntity BillingAddress { get; set; }

		public AddressDbEntity ShippingAddress { get; set; }

		public ICollection<OrderDbEntity> Orders { get; set; }
	}
}