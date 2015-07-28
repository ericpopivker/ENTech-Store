using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using ENTech.Store.Database.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.AuthenticationModule.Requests;
using ENTech.Store.Services.AuthenticationModule.Responses;
using ENTech.Store.Services.CommandService;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Infrastructure.WebApi
{
	public abstract class ApiKeyAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly IExternalCommandService _externalCommandService = new ExternalCommandService(new CommandFactory(), DbContextScope.CurrentDbContext);

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext == null)
				throw new ArgumentNullException("actionContext");

			var httpRequestMessage = actionContext.Request;

			if (httpRequestMessage.Method == HttpMethod.Options)
				return;

			if (actionContext.Request.Headers.Authorization == null)
			{
				HandleUnauthorizedRequest(actionContext);
				return;
			}

			var authorizationParameters = AuthorizationParameters(actionContext);

			if (authorizationParameters.ContainsKey("ApiKey") == false)
			{
				HandleUnauthorizedRequest(actionContext);
				return;
			}

			string apiKey = authorizationParameters["ApiKey"];
			
			var authenticateResponse = _externalCommandService.Execute(new AuthenticateApiKeyRequest
			{
				ApiKey = apiKey
			});

			if (authenticateResponse is ErrorResponseStatus<AuthenticateApiKeyResponse>)
			{
				HandleUnauthorizedRequest(actionContext);
				return;
			}

			var successfulAuthenticateResponse = (OkResponseStatus<AuthenticateApiKeyResponse>) authenticateResponse;

			if (successfulAuthenticateResponse.Response == null || successfulAuthenticateResponse.Response.IsAuthenticated == false)
			{
				HandleUnauthorizedRequest(actionContext);
				return;
			}

			var partnerDto = successfulAuthenticateResponse.Response.Partner;
			var apiKeyAuthResult = new ApiKeyAuthorizationResult(partnerDto.Id, partnerDto.Name);

			var protectedAuthorizeResult = HandleAuthorizeProtected(authorizationParameters, apiKeyAuthResult);

			if (protectedAuthorizeResult.IsSuccessful)
			{
				Thread.CurrentPrincipal = CreatePrincipal(apiKeyAuthResult, protectedAuthorizeResult);
			}
		}

		private static Dictionary<string, string> AuthorizationParameters(HttpActionContext actionContext)
		{
			var authorizationParameters =
				actionContext.Request.Headers.Authorization.Parameter.Split(' ')
					.ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
			return authorizationParameters;
		}

		protected virtual IPrincipal CreatePrincipal(ApiKeyAuthorizationResult response, AuthorizationResult protectedAuthorizeResult)
		{
			return new GenericPrincipal(new PartnerIdentity
			{
				AuthenticationType = "ApiKeyAuthentication",
				IsAuthenticated = true,
				Name = response.PartnerName,
				PartnerId = response.PartnerId
			}, new string[0]);
		}

		protected abstract AuthorizationResult HandleAuthorizeProtected(Dictionary<string, string> authorizationParametersDictionary, ApiKeyAuthorizationResult apiKeyAuthorizationResult);

		protected class ApiKeyAuthorizationResult
		{
			public ApiKeyAuthorizationResult(int id, string name)
			{
				PartnerId = id;
				PartnerName = name;
			}

			public int PartnerId { get; private set; }
			public string PartnerName { get; private set; }
		}
	}
}