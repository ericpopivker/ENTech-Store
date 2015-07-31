using AutoMapper;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Entities.StoreModule;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToProductCategoryMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<ProductCategoryDbEntity, ProductCategory>();
		}
	}
}