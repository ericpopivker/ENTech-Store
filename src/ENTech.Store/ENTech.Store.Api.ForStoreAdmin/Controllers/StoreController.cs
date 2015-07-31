using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Infrastructure.WebApi.Implementation;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.StoreModule.Expand.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	[RoutePrefix("v1/store-admin-api/stores")]
	[PublicAuthorize]
	public class StoreController : ApiController
	{
		private readonly IExternalCommandService _externalCommandService;
		private readonly IDtoExpander _dtoExpander;

		public StoreController(IExternalCommandService externalCommandService, IDtoExpander dtoExpander)
		{
			_externalCommandService = externalCommandService;
			_dtoExpander = dtoExpander;
		}

		[HttpPost]
		[ResponseType(typeof(StoreCreateResponse))]
		[Route("create")]
		public HttpResponseMessage Create([FromBody]StoreCreateRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[Route("{Id:int}")]
		[ResponseType(typeof(StoreGetByIdResponse))]
		public HttpResponseMessage GetById([FromUri] StoreGetByIdRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpPut]
		[Route("{Id:int}")]
		[ResponseType(typeof(StoreUpdateResponse))]
		public HttpResponseMessage Update([FromBody] StoreUpdateRequest request, int id)
		{
			request.StoreId = id;
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[Route("")]
		[ResponseType(typeof(StoreFindResponse))]
		public HttpResponseMessage Find([FromUri]StoreFindRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[Route("{Id:int}/expand")]
		public HttpResponseMessage GetById(int Id)
		{
			var result = _dtoExpander.LoadAndExpand(Id, new List<ExpandOption<StoreExpandableDto>>());
			return Request.CreateResponse(result);
		}
	}
}