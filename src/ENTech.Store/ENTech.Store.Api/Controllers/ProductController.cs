using System.Collections.Generic;
using System.Web.Http;

namespace ENTech.Store.Api.Controllers
{
	[RoutePrefix("1.0/store-admin-api/products")]
	public class ProductController : ApiController
	{
		//private IExternalCommandService _externalCommandService;

		//public ProductController(IExternalCommandService externalCommandService)
		//{
		//	_externalCommandService = externalCommandService;
		//}

		[HttpPost]
		public void Create([FromBody]string value)
		{
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