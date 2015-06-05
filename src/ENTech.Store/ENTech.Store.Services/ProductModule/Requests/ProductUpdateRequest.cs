using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductUpdateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		public int Id { get; set; }
		public ProductUpdateDto Product { get; set; }
	}
}