using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	[RoutePrefix("1.0/store-admin-api/stores")]
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

		[HttpPost]
		[ResponseType(typeof(StoreCreateResponse))]
		[Route("create")]
		public HttpResponseMessage Create([FromBody]StoreCreateRequest request)
		{
			var response = _anonymousExternalCommandService.Execute<StoreCreateRequest, StoreCreateResponse, StoreCreateCommand>(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[Route("{Id:int}")]
		[ResponseType(typeof(StoreGetByIdResponse))]
		public HttpResponseMessage GetById([FromBody] StoreGetByIdRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<StoreGetByIdRequest, StoreGetByIdResponse, StoreGetByIdCommand>(request);
			return Request.CreateResponse(response);
		}

		[HttpPut]
		[Route("{Id:int}")]
		[ResponseType(typeof(StoreUpdateResponse))]
		public HttpResponseMessage GetById(StoreUpdateRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<StoreUpdateRequest, StoreUpdateResponse, StoreUpdateCommand>(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[Route("")]
		[ResponseType(typeof(StoreFindResponse))]
		public HttpResponseMessage Find([FromUri]StoreFindRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<StoreFindRequest, StoreFindResponse, StoreFindCommand>(request);
			return Request.CreateResponse(response);
		}

	}
}