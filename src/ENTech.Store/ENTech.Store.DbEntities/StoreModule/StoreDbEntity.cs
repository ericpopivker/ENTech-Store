using System;
using System.Collections.Generic;
using ENTech.Store.DbEntities.CustomerModule;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.OrderModule;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.DbEntities.StoreModule
{
	public class StoreDbEntity : IDbEntity
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

		public int? AddressId { get; set; }

		public AddressDbEntity Address { get; set; }

		public ICollection<CustomerDbEntity> Customers { get; set; }

		public ICollection<ProductDbEntity> Products { get; set; }
		
		public ICollection<OrderDbEntity> Orders { get; set; }
	}
}
