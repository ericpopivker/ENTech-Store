using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Infrastructure.Services.Validators;

//Interesting
//http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/action-results
//to make it reusable: IHttpActionResult

//HttpType in response HttpStatusCode
// http://stackoverflow.com/questions/10655350/returning-http-status-code-from-asp-net-mvc-4-web-api-controller


//this is it
//return this.Request.CreateResponse<ComputingDevice>(
		//		HttpStatusCode.OK, computingDevice);
		//}

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	[RoutePrefix("v1/store-admin-api/test")]
	public class TestController : ApiController
	{
	
		public TestController()
		{
		
		}

		[HttpPost]
		[Route("")]
		public HttpResponseMessage Test([FromBody]TestRequest request)
		{
			switch (request.ReturnHttpStatusCode)
			{
				case (int) HttpStatusCode.OK:
					return this.Request.CreateResponse(new OkResponseStatus<TestResponse>(new TestResponse()));
				
				case (int) HttpStatusCode.InternalServerError:
					return this.Request.CreateResponse(new ErrorResponseStatus<TestResponse>(new InternalServerResponseError()));
				
				case (int) HttpStatusCode.BadRequest:

					var error = new InvalidArgumentsResponseError(new List<ResponseArgumentError>
					{
						new ResponseArgumentError
							(
							ArgumentName<TestRequest>.For(r => r.Val1),
							new ArgumentErrorStub()
							),
						new ResponseArgumentError
							(
							ArgumentName<TestRequest>.For(r => r.Val2),
							new ArgumentErrorStub()
							)

					});

					return this.Request.CreateResponse(new ErrorResponseStatus<TestResponse>(error));
			}

			throw new ArgumentOutOfRangeException("Http Status Code:" + request.ReturnHttpStatusCode + ". Not supported.");
		}

		public class ArgumentErrorStub : ArgumentError
		{
			public ArgumentErrorStub() : base(10001)
			{
			}

			protected override string ErrorMessageTemplate
			{
				get { return "Stub Argument Error"; }
			}
		}



		public class TestRequest
		{
			public int ReturnHttpStatusCode { get; set; }

			public string Val1 { get; set; }

			public string Val2 { get; set; }

		}


		public class TestResponse : IResponse
		{
			public bool ItIsOkResponse { get; set; }

			public TestResponse()
			{
				ItIsOkResponse = true;
			}

		}

	
	}

	
}