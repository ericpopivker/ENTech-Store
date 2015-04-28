using System.ComponentModel.DataAnnotations;
using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Requests
{
	public class ProductUpdateRequest : ExternalRequestBase
	{
		[Required]
		public ProductDto Product { get; set; }
	}
}