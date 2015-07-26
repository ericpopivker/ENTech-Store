using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.AuthenticationModule.Requests
{
	public class AuthenticateApiKeyRequest : IRequest
	{
		public string ApiKey { get; set; }
	}
}