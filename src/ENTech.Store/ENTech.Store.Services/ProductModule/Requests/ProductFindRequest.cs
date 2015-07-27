using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Requests;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductFindRequest :
		FindRequestBase<ProductFindResponse, ProductLoadOption, ProductSortField, ProductFindCriteriaDto>
	{
		public string Name { get; set; }
		public int StoreId { get; set; }
	}
}