using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Services.External.ForPartner.PartnerModule.MapProfiles;
using ENTech.Store.Services.External.ForPartner.PartnerModule.Requests;
using ENTech.Store.Services.External.ForPartner.PartnerModule.Responses;
using ENTech.Store.Services.Internal.PartnerModule;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule.Commands
{
	internal class PartnerAuthenticateCommand :
		AnonymousExternalCommandBase
			<PartnerAuthenticateRequest, PartnerAuthenticateResponse, Internal.PartnerModule.Requests.PartnerAuthenticateRequest,
				Internal.PartnerModule.Responses.PartnerAuthenticateResponse>
	{
		private readonly IPartnerService _partnerService;

		public PartnerAuthenticateCommand(IPartnerService partnerService)
		{
			_partnerService = partnerService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		protected override bool RequireTransaction
		{
			get { return false; }
		}

		protected override void LimitDbContext(IDbContext dbContext)
		{
		}

		public override Internal.PartnerModule.Responses.PartnerAuthenticateResponse ExecuteInternal(
			Internal.PartnerModule.Requests.PartnerAuthenticateRequest internalRequest)
		{
			return _partnerService.Authenticate(internalRequest);
		}
	}
}
