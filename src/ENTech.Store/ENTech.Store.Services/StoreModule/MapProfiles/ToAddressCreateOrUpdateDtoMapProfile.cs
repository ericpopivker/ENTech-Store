using AutoMapper;
using ENTech.Store.Services.GeoModule.Dtos;
using AddressDto = ENTech.Store.Services.StoreModule.Dtos.AddressDto;

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