using ENTech.Store.Infrastructure.Entities;
using ProtoBuf;

namespace ENTech.Store.Entities.GeoModule
{
	[ProtoContract]
	public class Address : IDomainEntity
	{
		public int Id { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Street2 { get; set; }

		public int? StateId { get; set; }

		public string StateOther { get; set; }

		public int CountryId { get; set; }
	}
}
