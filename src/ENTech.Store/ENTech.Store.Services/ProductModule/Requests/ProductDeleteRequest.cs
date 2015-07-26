using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductDeleteRequest : IRequest
	{
		public int Id { get; set; }
	}
}