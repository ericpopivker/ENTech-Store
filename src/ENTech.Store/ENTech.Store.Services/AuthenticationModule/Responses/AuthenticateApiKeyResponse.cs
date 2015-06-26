using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.AuthenticationModule.Dtos;

namespace ENTech.Store.Services.AuthenticationModule.Responses
{
	public class AuthenticateApiKeyResponse : ResponseBase
	{
		public bool IsAuthenticated { get; set; }

		public Error Error { get; set; }

		public PartnerDto Partner { get; set; }
	}
}