using AutoMapper;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToAddressCreateOrUpdateDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<AddressDto, AddressCreateOrUpdateDto>();
		}
	}
}