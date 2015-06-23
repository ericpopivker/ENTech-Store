﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ENTech.Store.Api;
using ENTech.Store.Api.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ENTech.Store.Api.Startup))]
namespace ENTech.Store.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OAuthConfig.ConfigureOAuthTokenGeneration(app);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

        }

    }
}