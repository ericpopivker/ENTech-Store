namespace ENTech.Store.Infrastructure.Services.Responses.Statuses
{
	public class OkResponseStatus<TResponse> : IResponseStatus<TResponse> where TResponse : IResponse
	{
		public TResponse Response { get; set; }

		public OkResponseStatus(TResponse response)
		{
			Verify.Argument.IsNotNull(response, "response");

			this.Response = response;
		}
	}
}
