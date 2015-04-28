using AutoMapper;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule.MapProfiles
{
	class InternalToExternalResponseMapProfile:Profile
	{
		protected override void Configure()
		{
			CreateMap<InternalResponse, ExternalResponse>()
				.ForMember(e => e.IsSuccess, o => o.MapFrom(i => i.IsSuccess))
				.ForMember(e => e.Error, o => o.MapFrom(i => i.Error))
				.ForMember(e => e.ArgumentErrors, o => o.MapFrom(i => i.ArgumentErrors));

			CreateMap<Internal.PartnerModule.Responses.PartnerAuthenticateResponse, Responses.PartnerAuthenticateResponse>()
				.ForMember(e => e.ExpireAt, o => o.MapFrom(i => i.ExpireAt))
				.ForMember(e => e.Token, o => o.MapFrom(i => i.Token));
		}
	}
}
