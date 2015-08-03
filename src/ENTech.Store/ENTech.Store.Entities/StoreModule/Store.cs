using System;
using ENTech.Store.Infrastructure.Entities;
using ProtoBuf;

namespace ENTech.Store.Entities.StoreModule
{
	[ProtoContract]
	public class Store : IDomainEntity, IAuditable, ILogicallyDeletable
	{
		[ProtoMember(1)]
		public int Id { get; set; }

		[ProtoMember(2)]
		public DateTime CreatedAt { get; set; }
		[ProtoMember(3)]
		public DateTime LastUpdatedAt { get; set; }

		[ProtoMember(4)]
		public bool IsDeleted { get; set; }
		[ProtoMember(5)]
		public DateTime? DeletedAt { get; set; }

		[ProtoMember(6)]
		public string Name { get; set; }
		[ProtoMember(7)]
		public string Logo { get; set; }
		[ProtoMember(8)]
		public string Phone { get; set; }
		[ProtoMember(9)]
		public string Email { get; set; }
		[ProtoMember(10)]
		public string TimezoneId { get; set; }

		[ProtoMember(11)]
		public int? AddressId { get; set; }
		[ProtoMember(12)]
		public int[] ProductIds { get; set; }

		[ProtoMember(13)]
		public int? SettingsId { get; set; }
	}
}
