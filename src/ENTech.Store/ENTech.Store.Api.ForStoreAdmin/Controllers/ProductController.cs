using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	[RoutePrefix("v1/store-admin-api/products")]
	public class ProductController : ApiController
	{
		private readonly IExternalCommandService _externalCommandService;

		public ProductController(IExternalCommandService externalCommandService)
		{
			_externalCommandService = externalCommandService;
		}

		[HttpPost]
		[ResponseType(typeof(ProductCreateResponse))]
		public HttpResponseMessage Create([FromBody]ProductCreateRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}


		[HttpPut]
		[ResponseType(typeof(ProductUpdateResponse))]
		[Route("{Id:int}")]
		public HttpResponseMessage Update(int id, [FromBody]ProductUpdateRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpPost]
		[ResponseType(typeof(ProductDeleteResponse))]
		[Route("{Id:int}/delete")]
		public HttpResponseMessage Delete([FromBody]ProductDeleteRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[ResponseType(typeof(ProductGetByIdResponse))]
		[Route("{Id:int}")]
		public HttpResponseMessage GetById([FromUri]ProductGetByIdRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[ResponseType(typeof(ProductFindResponse))]
		[Route("")]
		public HttpResponseMessage Find([FromUri]ProductFindRequest request)
		{
			var response = _externalCommandService.Execute(request);
			return Request.CreateResponse(response);
		}

	}

	
}