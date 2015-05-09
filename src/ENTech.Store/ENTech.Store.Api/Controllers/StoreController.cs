using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using ENTech.Store.Api.ModelBinders;

namespace ENTech.Store.Api.Controllers
{
	[RoutePrefix("v1/store-admin-api/stores")]
	public class StoreController : ApiController
	{

		/// <summary>
		/// Store Create Request for creating store
		/// </summary>
		public class StoreCreateRequest
		{
			public StoreDto Store { get; set; }
		}

		/// <summary>
		/// Store Create Response for creating store
		/// </summary>
		public class StoreCreateResponse 
		{
			/// <summary>
			/// Did method work
			/// </summary>
			public bool IsSuccess { get; set; }

			public string ResponseErrorMessage { get; set; }

			public string ArgumentErrors { get; set; }

			public int StoreId { get; set; }
				
		}


		public class StoreDto
		{
			public string Name { get; set; }

			public string Phone { get; set; }

			public string Email { get; set; }
		}


		/// <summary>
		/// Create da Store ya ll
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		[ResponseType(typeof(StoreCreateResponse))]
		public HttpResponseMessage Create([ModelBinder(typeof(StoreCreateRequestModelBinder))]StoreCreateRequest request)
		{
			var response = new StoreCreateResponse();
		
			// Authenticate (or check if token is valid)
			// TODO

			// Parse Store into StoreCreateRequest
			// DONE
			
			// Create Unit Of Work

			// Limit Db Context as needed. Not needed in this case

			// Validate Request

			// Execute Request
			
			// Return response use Http Codes
			response.IsSuccess = true;
			return Request.CreateResponse(response);
		}



		[HttpGet]
		[Route("{Id:int}")]
		public string GetById(int id)
		{
			return "value";
		}



		[HttpGet]
		[Route("")]
		public IEnumerable<string> Find()
		{
			return new string[] { "value1", "value2" };
		}

	}

	
}