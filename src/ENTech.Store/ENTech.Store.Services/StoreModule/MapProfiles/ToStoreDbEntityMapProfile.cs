using AutoMapper;
using ENTech.Store.DbEntities.StoreModule;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToStoreDbEntityMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Entities.StoreModule.Store, StoreDbEntity>();
		}
	}
}