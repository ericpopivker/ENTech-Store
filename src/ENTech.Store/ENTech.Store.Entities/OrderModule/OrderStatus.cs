using ENTech.Store.Infrastructure.Attributes;

namespace ENTech.Store.Entities.OrderModule
{
	public enum OrderStatus
	{
		Created = 1,
		Submitted,
		Paid,
		[StringValue("Payment Failed")]
		PaymentFailed,
		Refunded
	}
}