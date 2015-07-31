using AutoMapper;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Entities.StoreModule;

namespace ENTech.Store.Services.ProductModule.MapProfiles
{
	public class ToProductProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<ProductDbEntity, Product>();
		}
	}
}