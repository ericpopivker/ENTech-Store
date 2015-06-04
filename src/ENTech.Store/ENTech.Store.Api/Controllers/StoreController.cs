using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Api.Controllers
{
	[System.Web.Http.RoutePrefix("1.0/store-admin-api/stores")]
	public class StoreController : ApiController
	{
		private readonly IExternalCommandService<AnonymousSecurityInformation> _anonymousExternalCommandService;

		public StoreController(IExternalCommandService<AnonymousSecurityInformation> anonymousExternalCommandService)
		{
			_anonymousExternalCommandService = anonymousExternalCommandService;
		}

		[System.Web.Http.HttpPost]
		[ResponseType(typeof(StoreCreateRequest))]
		public HttpResponseMessage Create([FromBody]StoreCreateRequest request)
		{
			var response = _anonymousExternalCommandService.Execute<StoreCreateRequest, StoreCreateResponse, StoreCreateCommand>(request);
			return Request.CreateResponse(response);
		}



		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("{Id:int}")]
		public string GetById(int id)
		{
			return "value";
		}



		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("")]
		public IEnumerable<string> Find()
		{
			return new string[] { "value1", "value2" };
		}

	}
}