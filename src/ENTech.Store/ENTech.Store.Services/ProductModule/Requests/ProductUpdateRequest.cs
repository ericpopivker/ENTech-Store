using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductUpdateRequest : IRequest
	{
		public int Id { get; set; }
		public ProductUpdateDto Product { get; set; }
	}
}