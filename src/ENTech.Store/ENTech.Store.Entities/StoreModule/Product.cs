﻿using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.StoreModule
{
	public class Product : IDomainEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }
	}
}