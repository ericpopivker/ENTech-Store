using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Api.Controllers
{
	[RoutePrefix("1.0/store-admin-api/products")]
	public class ProductController : ApiController
	{
		private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

		public ProductController(IExternalCommandService<BusinessAdminSecurityInformation> businessAdminExternalCommandService)
		{
			_businessAdminExternalCommandService = businessAdminExternalCommandService;
		}

		[HttpPost]
		[ResponseType(typeof(ProductCreateResponse))]
		[Route("create")]
		public HttpResponseMessage Create([FromBody]ProductCreateRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductCreateRequest, ProductCreateResponse, ProductCreateCommand>(request);
			return Request.CreateResponse(response);
		}
	}
}