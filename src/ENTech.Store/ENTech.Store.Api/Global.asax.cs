using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Api
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
			RequestValidatorErrorMessagesDictionary.RegisterAll();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.RegisterComponents);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
