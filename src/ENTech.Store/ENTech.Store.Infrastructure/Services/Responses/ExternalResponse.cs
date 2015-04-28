namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class ExternalResponse : IResponse
	{
		public bool IsSuccess { get; set; }

		public Error Error { get; set; }

		public virtual ArgumentErrorsCollection ArgumentErrors { get; set; }

		public ExternalResponse()
		{
			ArgumentErrors = new ArgumentErrorsCollection();
		}
	}
}