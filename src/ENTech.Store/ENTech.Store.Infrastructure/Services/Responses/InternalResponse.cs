namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class InternalResponse : IResponse
	{
		public bool IsSuccess { get; set; }

		public Error Error { get; set; }

		public virtual ArgumentErrorsCollection ArgumentErrors { get; set; }

		public InternalResponse()
		{
			ArgumentErrors = new ArgumentErrorsCollection();
		}
	}
}
