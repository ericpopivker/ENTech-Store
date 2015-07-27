using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductDeleteRequest : IRequest<ProductDeleteResponse>
	{
		public int Id { get; set; }
	}
}