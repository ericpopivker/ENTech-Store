using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Dtos;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Responses
{
	public class ProductCreateResponse : ExternalResponse
	{
		public ProductDto Product { get; set; }
	}
}