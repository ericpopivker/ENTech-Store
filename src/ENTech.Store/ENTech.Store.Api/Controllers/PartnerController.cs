using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ENTech.Store.Api.Controllers
{
	[RoutePrefix("v1/partner-api")]
	public class PartnerController : ApiController
	{
		

		[HttpPost]
		[Route("authenticate")]
		public void Authenticate([FromBody]string value)
		{
			
		}

	}

	
}