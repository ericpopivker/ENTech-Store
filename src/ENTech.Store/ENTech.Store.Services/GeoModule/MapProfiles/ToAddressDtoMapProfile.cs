using AutoMapper;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Services.GeoModule.Dtos;

namespace ENTech.Store.Services.GeoModule.MapProfiles
{
	public class ToAddressDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Address, AddressDto>();
		}
	}
}