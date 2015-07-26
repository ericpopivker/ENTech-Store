using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.WebApi
{
	public class PublicAuthorizeAttribute : ApiKeyAuthorizeAttribute
	{
		protected override AuthorizationResult HandleAuthorizeProtected(Dictionary<string, string> authorizationParametersDictionary, ApiKeyAuthorizationResult apiKeyAuthorizationResult)
		{
			return AuthorizationResult.Success();
		}
	}
}