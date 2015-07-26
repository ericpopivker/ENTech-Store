using AutoMapper;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Services.AuthenticationModule.Dtos;

namespace ENTech.Store.Services.AuthenticationModule.MapProfiles
{
	public class ToPartnerDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Partner, PartnerDto>();
		}
	}
}