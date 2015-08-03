using System;
using ENTech.Store.Infrastructure.Entities;
using ProtoBuf;

namespace ENTech.Store.Entities.StoreModule
{
	[ProtoContract]
	public class ProductCategory : IDomainEntity, IAuditable
	{
		[ProtoMember(1)]
		public int Id { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }
		[ProtoMember(3)]
		public DateTime CreatedAt { get; set; }
		[ProtoMember(4)]
		public DateTime LastUpdatedAt { get; set; }
	}
}