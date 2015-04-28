using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.PartnerModule.Requests;

namespace ENTech.Store.Services.External
{
	public abstract class PartnerAuthExternalCommandBase<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse> :
		ExternalCommandBase<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse>
		where TExternalRequest : ExternalRequestBase, IExternalRequest
		where TExternalResponse : ExternalResponse, new()
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : InternalResponse
	{
		private readonly IPartnerService _partnerService;
		protected int? PartnerId { get; set; }


		protected PartnerAuthExternalCommandBase(IPartnerService partnerService)
		{
			_partnerService = partnerService;
		}

		protected override bool Authenticate()
		{
			var partnerVerificationResponse =
				_partnerService.VerifyAuthentication(new PartnerVerifyAuthenticationRequest { PartnerToken = Request.PartnerToken });

			if (!partnerVerificationResponse.IsSuccess)
			{
				ErrorResponse(partnerVerificationResponse.Error, partnerVerificationResponse.ArgumentErrors);
				return false;
			}

			PartnerId = partnerVerificationResponse.PartnerId;

			return true;
		}
	}
}
