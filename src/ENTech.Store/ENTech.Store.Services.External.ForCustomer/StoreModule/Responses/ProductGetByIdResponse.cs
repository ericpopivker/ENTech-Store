using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Responses
{
	public class ProductGetByIdResponse : ExternalResponse
	{
		public ProductDto Product { get; set; }
	}
}
