using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductUpdateRequest : IRequest<ProductUpdateResponse>
	{
		public int Id { get; set; }
		public ProductUpdateDto Product { get; set; }
	}
}