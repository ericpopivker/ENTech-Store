using System.Collections.Generic;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.Dtos
{
	public class StoreExpandableDto : StoreDto, IExpandableDto
	{
		public AddressDto Address { get; set; }
		public IEnumerable<ProductDto> Products { get; set; }
	}
}