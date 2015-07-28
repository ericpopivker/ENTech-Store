using AutoMapper;
using ENTech.Store.Database.PartnerModule;
using ENTech.Store.Entities.PartnerModule;

namespace ENTech.Store.Services.AuthenticationModule.MapProfiles
{
	public class ToPartnerMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<PartnerDbEntity, Partner>();
		}
	}
}