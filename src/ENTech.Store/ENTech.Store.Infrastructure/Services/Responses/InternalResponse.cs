namespace ENTech.Store.Infrastructure.Services.Responses
{
	public abstract class InternalResponse : IResponse
	{
		public bool IsSuccess { get; set; }

		public Error Error { get; set; }

		public virtual ArgumentErrorsCollection ArgumentErrors { get; set; }

		
		protected InternalResponse()
		{
			ArgumentErrors = new ArgumentErrorsCollection();
		}
	}
}
