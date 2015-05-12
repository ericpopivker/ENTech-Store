using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ENTech.Store.Api
{
    [Route("api")]
    public class ApiVersionController : ApiController
    {

        public IEnumerable<ApiVersionDescriptor> Get()
        {
            return ApiVersions.GetVersions();
        }

        // GET: api/Api/5
        [Route("api/{v}")]
        public ApiVersionDescriptor Get(string v)
        {
            return ApiVersions.GetVersions().FirstOrDefault(c => c.Name == v);
        }
   
    }
}
