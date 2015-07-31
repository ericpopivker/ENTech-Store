using AutoMapper;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.MapProfiles
{
	public class ToProductDtoProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Product, ProductDto>();
		}
	}
}