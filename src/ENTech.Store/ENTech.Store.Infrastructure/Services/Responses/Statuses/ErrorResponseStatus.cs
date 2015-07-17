using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Responses.Statuses
{
	public class ErrorResponseStatus<TResponse> : IResponseStatus<TResponse> where TResponse : IResponse
	{
		public ResponseError Error { get; set; }

		public ErrorResponseStatus(ResponseError error)
		{
			Verify.Argument.IsNotNull(error, "error");

			this.Error = error;
		}
	}
}
