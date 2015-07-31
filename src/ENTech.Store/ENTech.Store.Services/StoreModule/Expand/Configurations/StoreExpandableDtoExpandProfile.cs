using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Expand.Dtos;
using ENTech.Store.Services.StoreModule.Requests;

namespace ENTech.Store.Services.StoreModule.Expand.Configurations
{
	public class StoreExpandableDtoExpandProfile : ExpandProfile<StoreExpandableDto>
	{
		protected override void Configure()
		{
			LoadRoot()
				.IfSingle<StoreGetByIdRequest>()
				.IfMultiple<StoreFindByIdsRequest>();

			ExpandProperty(x=>x.Address)
 				.FromIdentityProperty(x=>x.AddressId)
				.IfSingle<AddressGetByIdRequest>()
				.IfMultiple<AddressFindByIdsRequest>();

			ExpandProperty(x => x.Products)
				.FromIdentityProperty(x => x.ProductIds)
				.IfSingle<ProductGetByIdRequest>()
				.IfMultiple<ProductFindByIdsRequest>();
		}
	}
	public class ProductExpandableDtoExpandProfile : ExpandProfile<ProductExpandableDto>
	{
		protected override void Configure()
		{
			LoadRoot()
				.IfSingle<ProductGetByIdRequest>()
				.IfMultiple<ProductFindByIdsRequest>();

			ExpandProperty(x => x.Category)
				.FromIdentityProperty(x => x.CategoryId)
				.IfSingle<ProductCategoryGetByIdRequest>();
		}
	}
}