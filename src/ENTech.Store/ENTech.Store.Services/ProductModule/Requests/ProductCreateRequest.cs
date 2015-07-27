using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductCreateRequest : IRequest<ProductCreateResponse>
	{
		[Required]
		public ProductCreateDto Product { get; set; }
	}
}