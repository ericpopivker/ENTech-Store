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
		private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

		public StoreController(IExternalCommandService<AnonymousSecurityInformation> anonymousExternalCommandService, 
			IExternalCommandService<BusinessAdminSecurityInformation> businessAdminExternalCommandService)
		{
			_anonymousExternalCommandService = anonymousExternalCommandService;
			_businessAdminExternalCommandService = businessAdminExternalCommandService;
		}

		[System.Web.Http.HttpPost]
		[ResponseType(typeof(StoreCreateResponse))]
		public HttpResponseMessage Create([FromBody]StoreCreateRequest request)
		{
			var response = _anonymousExternalCommandService.Execute<StoreCreateRequest, StoreCreateResponse, StoreCreateCommand>(request);
			return Request.CreateResponse(response);
		}



		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("{Id:int}")]
		[ResponseType(typeof(StoreGetByIdResponse))]
		public HttpResponseMessage GetById(StoreGetByIdRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<StoreGetByIdRequest, StoreGetByIdResponse, StoreGetByIdCommand>(request);
			return Request.CreateResponse(response);
		}

	[System.Web.Http.HttpGet]
		[System.Web.Http.Route("")]
		public IEnumerable<string> Find()
		{
			return new string[] { "value1", "value2" };
		}

	}
}