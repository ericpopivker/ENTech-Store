using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Responses.Statuses
{
	public class ErrorResponseStatus<TResponse> : IResponseStatus<TResponse> where TResponse : ResponseBase
	{
		public Error Error { get; set; }

		public ErrorResponseStatus(Error error)
		{
			Verify.Argument.IsNotNull(error, "error");

			this.Error = Error;
		}
	}
}
