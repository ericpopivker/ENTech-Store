using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.AuthenticationModule.Dtos;

namespace ENTech.Store.Services.AuthenticationModule.Responses
{
	public class AuthenticateApiKeyResponse : ResponseBase
	{
		public PartnerDto Partner { get; set; }
	}
}