using AutoMapper;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToAddressDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<GeoModule.Dtos.AddressDto, AddressDto>();
		}
	}
}