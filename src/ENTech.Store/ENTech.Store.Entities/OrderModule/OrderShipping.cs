using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	public enum OrderShippingStatus
	{
		Processing,
		Sent
	}

	[Table("OrderShipping")]
	public class OrderShipping : IDomainEntity
	{
		public int Id { get; set; }

		public string Instructions { get; set; }

		[Required]
		public int? AddressId { get; set; }

		[ForeignKey("AddressId")]
		public virtual Address Address { get; set; }

		[Column("StatusId")]
		public OrderShippingStatus Status { get; set; }
	}
}