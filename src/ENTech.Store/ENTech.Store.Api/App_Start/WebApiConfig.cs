using System.Web.Http;
using ENTech.Store.Api.App_Data;
using ENTech.Store.Infrastructure;

namespace ENTech.Store.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
			config.DependencyResolver = new UnityResolver(IoC.Container);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }


}


namespace EventGrid.Infrastructure
{
}