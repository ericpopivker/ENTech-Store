using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace ENTech.Store.Api.App_Start
{

    public class OAuthConfig
    {
        public static  void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomOAuthProvider.CustomFormat("http://localhost:59822")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
         
         
            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AccessTokenFormat = new CustomOAuthProvider.CustomFormat("http://localhost:59822")
            });
        }
    }

}