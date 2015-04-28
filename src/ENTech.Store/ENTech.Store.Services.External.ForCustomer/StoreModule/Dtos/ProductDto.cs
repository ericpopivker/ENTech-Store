using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public bool IsActive { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int CategoryId { get; set; }

		[Required]
		public string Name { get; set; }

		public List<ProductVariantDto> Variants { get; set; }

		public List<ProductOptionDto> Options { get; set; }

		public List<ProductPhotoDto> Photos { get; set; }
	}
}
