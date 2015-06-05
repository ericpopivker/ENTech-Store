using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductCreateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		public ProductCreateDto Product { get; set; }
	}
}