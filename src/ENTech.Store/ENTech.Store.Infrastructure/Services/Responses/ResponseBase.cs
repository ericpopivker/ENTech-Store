namespace ENTech.Store.Infrastructure.Services.Responses
{
	public abstract class ResponseBase : IResponse
	{
		public bool IsSuccess { get; set; }

		public ResponseError Error { get; set; }

		public virtual ArgumentErrorsCollection ArgumentErrors { get; set; }

		
		protected ResponseBase()
		{
			ArgumentErrors = new ArgumentErrorsCollection();
		}
	}
}
