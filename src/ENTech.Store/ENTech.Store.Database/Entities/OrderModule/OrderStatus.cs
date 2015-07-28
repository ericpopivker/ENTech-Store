namespace ENTech.Store.Database.OrderModule
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