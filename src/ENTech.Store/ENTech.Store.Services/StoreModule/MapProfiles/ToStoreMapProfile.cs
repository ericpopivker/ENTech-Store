using System.Linq;
using AutoMapper;
using ENTech.Store.Database.Entities.StoreModule;

namespace ENTech.Store.Services.StoreModule.MapProfiles
{
	public class ToStoreMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<StoreDbEntity, Entities.StoreModule.Store>()
				.ForMember(to=>to.ProductIds, must=>must.MapFrom(from=>from.Products.Select(x=>x.Id).ToList()));
		}
	}
}