using System.ComponentModel.DataAnnotations;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductCreateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		[Required]
		public ProductCreateDto Product { get; set; }
	}
}