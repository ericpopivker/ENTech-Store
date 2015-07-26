using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductCreateRequest : IRequest
	{
		[Required]
		public ProductCreateDto Product { get; set; }
	}
}