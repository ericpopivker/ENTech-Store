using System.Collections.Generic;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Dtos
{
	public class ProductVariantDto
	{
		public int Id { get; set; }

		public int QuantityInStock { get; set; }

		public decimal SalePrice { get; set; }

		public decimal Price { get; set; }

		public float Weight { get; set; }

		public string Status { get; set; }

		public string Sku { get; set; }

		public ProductPhotoDto Photo { get; set; }

		public List<ProductVariantOptionValueDto> OptionValues { get; set; }
	}
}
