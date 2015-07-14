namespace ENTech.Store.Projections.OrderModule
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