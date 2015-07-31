using AutoMapper;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.MapProfiles
{
	public class ToProductExpandableDtoProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<ProductDto, ProductExpandableDto>();

			CreateMap<ProductExpandableDto, ProductExpandableDto>();
		}
	}
}