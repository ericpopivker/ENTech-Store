using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.SharedModule.Requests;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductFindRequest : FindRequestBase<ProductLoadOption, ProductSortField, ProductFindCriteriaDto, BusinessAdminSecurityInformation>
	{ }
}