using System.Collections.Generic;
using System.Net;
using ENTech.Store.Services.StoreModule.Expand.Dtos;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ENTech.Store.Api.ForStoreAdmin.FunctionalTests
{
	[TestFixture]
	public class TestControllerTest
	{

		//http://stackoverflow.com/questions/6312970/restsharp-json-parameter-posting

		[Test]
		public void Create_When_valid_data_Then_returns_ok_response()
		{
			var client = new RestClient("http://localhost:50393");
			// client.Authenticator = new HttpBasicAuthenticator(username, password);

			var request = new RestRequest("v1/store-admin-api/test", Method.POST);
			request.AddParameter("ReturnHttpStatusCode", 200); // adds to POST or URL querystring based on Method
		
			// execute the request
			IRestResponse restResponse = client.Execute(request);
			var rawContent = restResponse.Content; // raw content as string


			var response = JsonConvert.DeserializeObject<TestResponse>(rawContent);

			Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
			Assert.IsTrue(response.ItIsOkResponse);
		}


		[Test]
		public void Create_When_internal_error_Then_returns_500_response()
		{
			var client = new RestClient("http://localhost:50393");

			var request = new RestRequest("v1/store-admin-api/test", Method.POST);
			request.AddParameter("ReturnHttpStatusCode", 500);

			// execute the request
			IRestResponse restResponse = client.Execute(request);
			var rawContent = restResponse.Content; // raw content as string

			var response = JsonConvert.DeserializeObject<InternalServerErrorResponse>(rawContent);

			Assert.AreEqual(HttpStatusCode.InternalServerError, restResponse.StatusCode);
			Assert.AreEqual( "Internal server error", response.ErrorMessage);
		}


		[Test]
		public void Create_When_invalid_request_Then_returns_400_response()
		{
			var client = new RestClient("http://localhost:50393");

			var request = new RestRequest("v1/store-admin-api/test", Method.POST);
			request.AddParameter("ReturnHttpStatusCode", 400);

			// execute the request
			IRestResponse restResponse = client.Execute(request);
			var rawContent = restResponse.Content; // raw content as string

			var response = JsonConvert.DeserializeObject<StoreExpandableDto>(rawContent);
		}



		public class TestResponse
		{
			public bool ItIsOkResponse { get; set; }
		}


		public class InternalServerErrorResponse
		{
			public string ErrorMessage { get; set; }
		}



		public class BadRequestErrorResponse
		{
			public int ErrorCode { get; set; }

			public string ErrorMessage { get; set; }

			public List<ArgumentError> ArgumentErrors { get; set; }


			public class ArgumentError
			{
				public string ArgumentName { get; set; }

				public int ErrorCode { get; set; }

				public string ErrorMessage { get; set; }

			}

		}


		
	}
}
