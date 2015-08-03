using System;
using ENTech.Store.Infrastructure.Entities;
using ProtoBuf;

namespace ENTech.Store.Entities.PartnerModule
{
	[ProtoContract]
	public class Partner : IDomainEntity, IAuditable
	{
		[ProtoMember(1)]
		public int Id { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }

		[ProtoMember(3)]
		public string Key { get; set; }

		[ProtoMember(4)]
		public string Secret { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }
	}
}
