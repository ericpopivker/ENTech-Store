using AutoMapper;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule.MapProfiles
{
	class ExternalToInternalRequestMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap
				<Requests.PartnerAuthenticateRequest, Internal.PartnerModule.Requests.PartnerAuthenticateRequest>()
				.ForMember(i => i.Key, o => o.MapFrom(e => e.Key))
				.ForMember(i => i.Secret, o => o.MapFrom(e => e.Secret));
		}
	}
}
