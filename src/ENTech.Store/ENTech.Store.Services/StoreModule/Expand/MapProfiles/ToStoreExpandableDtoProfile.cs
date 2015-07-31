using AutoMapper;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.MapProfiles
{
	public class ToStoreExpandableDtoProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<StoreDto, StoreExpandableDto>();
		}
	}
}