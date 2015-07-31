using System.Collections.Generic;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.Dtos
{
	public class StoreExpandableDto : StoreDto, IExpandableDto
	{
		public AddressDto Address { get; set; }
		public IEnumerable<ProductExpandableDto> Products { get; set; }
	}

	public class ProductExpandableDto : ProductDto, IExpandableDto
	{
		public ProductCategoryDto Category { get; set; }
	}

	public class ProductCategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}