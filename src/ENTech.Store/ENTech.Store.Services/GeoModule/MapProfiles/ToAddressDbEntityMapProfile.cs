using AutoMapper;
using ENTech.Store.Database.GeoModule;
using ENTech.Store.Entities.GeoModule;

namespace ENTech.Store.Services.GeoModule.MapProfiles
{
	public class ToAddressDbEntityMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Address, AddressDbEntity>();
		}
	}
}