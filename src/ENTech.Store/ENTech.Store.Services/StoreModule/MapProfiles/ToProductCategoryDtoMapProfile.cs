using AutoMapper;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToProductCategoryDtoMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<ProductCategory, ProductCategoryDto>();
		}
	}
}