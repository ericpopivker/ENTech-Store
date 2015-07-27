using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.AuthenticationModule.Responses;

namespace ENTech.Store.Services.AuthenticationModule.Requests
{
	public class AuthenticateApiKeyRequest : IRequest<AuthenticateApiKeyResponse>
	{
		public string ApiKey { get; set; }
	}
}