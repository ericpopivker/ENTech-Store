using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENTech.Store.Api
{

    public class ApiVersionDescriptor
    {
        public string Name;
        public string Date;
        public string Details;
        public string ExpirationDate;
    }

    public static class ApiVersions
    {
        public const string V1 = "api/v1";
        public const string V2 = "api/v2";

        public static IEnumerable<ApiVersionDescriptor> GetVersions()
        {
            yield return new ApiVersionDescriptor
            {
                Name = "v1",
                Date = "May 1",
                Details = "Initial version",
                ExpirationDate = "will stop supporting it on June 15"
            };

            yield return new ApiVersionDescriptor
            {
                Name = "v2",
                Date = "May 4",
                Details = "Some breaking changes details",
                ExpirationDate = ""
            };
        }



    }
}