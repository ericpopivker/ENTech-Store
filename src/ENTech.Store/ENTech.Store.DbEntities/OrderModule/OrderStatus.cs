namespace ENTech.Store.DbEntities.OrderModule
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