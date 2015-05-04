using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ENTech.Store.Api.Controllers
{
	[RoutePrefix("1.0/store-admin-api/stores")]
	public class StoreController : ApiController
	{

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