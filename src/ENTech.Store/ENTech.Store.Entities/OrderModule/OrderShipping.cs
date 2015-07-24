using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	public enum OrderShippingStatus
	{
		Processing,
		Sent
	}

	public class OrderShipping : IDomainEntity
	{
		public int Id { get; set; }

		public string Instructions { get; set; }

		public int? AddressId { get; set; }
		
		public OrderShippingStatus Status { get; set; }
	}
}