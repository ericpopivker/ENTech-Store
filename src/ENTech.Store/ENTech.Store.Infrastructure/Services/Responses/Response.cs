using System.Collections.Generic;
using System.Linq;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class Response
	{
		public bool IsSuccess { get; set; }
		public IEnumerable<ArgumentError> ArgumentErrors { get; set; }

		public Response()
		{
			ArgumentErrors = Enumerable.Empty<ArgumentError>();
		}

		public static Response Fail(IEnumerable<ArgumentError> errors)
		{
			return new Response
			{
				IsSuccess = false,
				ArgumentErrors = errors
			};
		}

		public static Response Success()
		{
			return new Response
			{
				IsSuccess = true
			};
		}
	}

	public class Response<T> : Response
	{
		public T Result { get; set; }
	}
}