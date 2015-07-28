using AutoMapper;
using ENTech.Store.Database.StoreModule;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToStoreMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<StoreDbEntity, Entities.StoreModule.Store>();
		}
	}
}