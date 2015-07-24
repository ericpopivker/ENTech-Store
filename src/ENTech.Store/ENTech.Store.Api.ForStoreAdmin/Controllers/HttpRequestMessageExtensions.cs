using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	public static class HttpRequestMessageExtensions
	{
		public static HttpResponseMessage CreateResponse<TResponse>(this HttpRequestMessage request,
			IResponseStatus<TResponse> responseStatus) where TResponse : IResponse
		{
			if (responseStatus is OkResponseStatus<TResponse>)
			{
				var response = ((OkResponseStatus<TResponse>) responseStatus).Response;
				return request.CreateResponse(HttpStatusCode.OK, response);
			}


			Verify.Argument.IsTrue(responseStatus is ErrorResponseStatus<TResponse>, "responseStatus");

			var errorResponseStatus = ((ErrorResponseStatus<TResponse>) responseStatus);
			var responseError = errorResponseStatus.Error;

			if (responseError.ErrorCode == CommonResponseErrorCode.InternalServerError)
			{
				var internalServerErrroResponse = new InternalServerErrorResponse {ErrorMessage = responseError.ErrorMessage};
				return request.CreateResponse(HttpStatusCode.InternalServerError, internalServerErrroResponse);
			}

			var badRequestResponse = ConvertResponseErrorToBadRequestErrorResponse(responseError);
			return request.CreateResponse(HttpStatusCode.BadRequest, badRequestResponse);
		}

		private static BadRequestErrorResponse ConvertResponseErrorToBadRequestErrorResponse(ResponseError responseError)
		{
			var response = new BadRequestErrorResponse();
			response.ErrorCode = responseError.ErrorCode;
			response.ErrorMessage = responseError.ErrorMessage;

			if (responseError is InvalidArgumentsResponseError)
			{
				response.ArgumentErrors = new List<BadRequestErrorResponse.ArgumentError>();
				foreach (var resArgumentError in ((InvalidArgumentsResponseError) responseError).ArgumentErrors)
				{
					var argError = new BadRequestErrorResponse.ArgumentError
					{
						ArgumentName = resArgumentError.ArgumentName,
						ErrorCode = resArgumentError.ArgumentError.ErrorCode,
						ErrorMessage = resArgumentError.ArgumentError.ErrorMessage
					};
					response.ArgumentErrors.Add(argError);
				}
			}

			return response;
		}


		public class InternalServerErrorResponse
		{
			public string ErrorMessage { get; set; }
		}


		public class BadRequestErrorResponse
		{
			public int ErrorCode { get; set; }

			public string ErrorMessage { get; set; }

			public IList<ArgumentError> ArgumentErrors { get; set; }


			public class ArgumentError
			{
				public string ArgumentName { get; set; }

				public int ErrorCode { get; set; }

				public string ErrorMessage { get; set; }

			}

		}
	}
}
