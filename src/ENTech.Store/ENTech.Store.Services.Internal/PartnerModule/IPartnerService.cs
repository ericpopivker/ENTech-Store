using ENTech.Store.Services.Internal.PartnerModule.Requests;
using ENTech.Store.Services.Internal.PartnerModule.Responses;

namespace ENTech.Store.Services.Internal.PartnerModule
{
	public interface IPartnerService
	{
		PartnerAuthenticateResponse Authenticate(PartnerAuthenticateRequest request);

		PartnerVerifyAuthenticationResponse VerifyAuthentication(PartnerVerifyAuthenticationRequest partnerToken);
	}
}