using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ENTech.Store.Api.Auth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;

namespace ENTech.Store.Api
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            const string allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user = new {id=1};

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = new ClaimsIdentity();
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Email, "1@1.1"));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Country, "Ru"));
            oAuthIdentity.AddClaim(new Claim("qDate",  DateTime.UtcNow.Ticks.ToString()));


            var ticket = new AuthenticationTicket(oAuthIdentity, null);
            context.Validated(ticket);
        }


        public class CustomFormat : ISecureDataFormat<AuthenticationTicket>
        {

            private readonly string _issuer = string.Empty;

            public CustomFormat(string issuer)
            {
                _issuer = issuer;
            }

            public string Protect(AuthenticationTicket data)
            {
                if (data == null)
                    throw new ArgumentNullException("data");

                //string audienceId = "aId";//ConfigurationManager.AppSettings["as:AudienceId"];
                //string symmetricKeyAsBase64 = "key";// ConfigurationManager.AppSettings["as:AudienceSecret"];

                //var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
                //var signingKey = new HmacSigningCredentials(keyByteArray);

                //var issued = data.Properties.IssuedUtc;
                //var expires = data.Properties.ExpiresUtc;

                var info = new AuthTokenInfo();
                info.UserId = data.Identity.FindFirst(ClaimTypes.Email).Value;
                info.Created = Convert.ToInt64( data.Identity.FindFirst("qDate").Value );

                var tObj = new AuthToken(info);
                return tObj.Token;
            }

            public AuthenticationTicket Unprotect(string protectedText)
            {
                var tObj = new AuthToken(protectedText);
                if (!tObj.IsValid(null))
                    throw new Exception("Invalid token");

                var info = tObj.Info;
                
                var oAuthIdentity = new ClaimsIdentity("Custom");
                
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, info.UserId));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Email, info.UserId));
                oAuthIdentity.AddClaim(new Claim("qDate", info.Created.ToString()));
                
                var ticket = new AuthenticationTicket(oAuthIdentity, null);
                return ticket;
            }
        }
    }
}