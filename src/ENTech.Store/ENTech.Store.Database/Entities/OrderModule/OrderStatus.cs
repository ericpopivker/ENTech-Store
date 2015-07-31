namespace ENTech.Store.Database.Entities.OrderModule
{
	public enum OrderStatus
	{
		Created = 1,
		Submitted,
		Paid,
		PaymentFailed,
		Refunded
	}
}