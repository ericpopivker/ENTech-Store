using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services;

namespace ENTech.Store.Api
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			StartupConfig.RegisterComponents();

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configure(UnityConfig.RegisterComponents);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}
