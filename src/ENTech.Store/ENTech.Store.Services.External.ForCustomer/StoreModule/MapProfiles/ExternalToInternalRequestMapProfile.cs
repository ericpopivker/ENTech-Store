using AutoMapper;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Requests;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.MapProfiles
{
	class ExternalToInternalRequestMapProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<SortFieldDto, Internal.StoreModule.Dtos.SortFieldDto>()
				.ForMember(i => i.IsDescending, o => o.MapFrom(e => e.IsDescending))
				.ForMember(i => i.SortField, o => o.MapFrom(e => e.SortField));

			CreateMap<ProductCriteriaDto, Internal.StoreModule.Dtos.ProductCriteriaDto>()
				.ForMember(i => i.CategoryId, o => o.MapFrom(e => e.CategoryId))
				.ForMember(i => i.PageSize, o => o.MapFrom(e => e.PageSize))
				.ForMember(i => i.PageIndex, o => o.MapFrom(e => e.PageIndex))
				.ForMember(i => i.SortField1, o => o.MapFrom(e => e.SortField1))
				.ForMember(i => i.SortField2, o => o.MapFrom(e => e.SortField2));

			CreateMap<ProductFindRequest, Internal.StoreModule.Requests.ProductFindRequest>()
				.ForMember(i => i.Criteria, o => o.MapFrom(e => e.Criteria))
				.ForMember(i => i.StoreId, o => o.MapFrom(e => e.StoreId));
		}
	}
}
