using AutoMapper;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToStoreDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Entities.StoreModule.Store, StoreDto>();
		}
	}
}