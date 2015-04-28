using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Services.Internal.StoreModule.Dtos;
using ENTech.Store.Services.Internal.StoreModule.Requests;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	internal class ProductCreateRequestBuilder :
		BuilderBase<ProductCreateRequest, ProductCreateRequestBuilder>
	{
		public ProductCreateRequestBuilder WithProduct(ProductDto product)
		{
			Product = product;
			return this;
		}

		public ProductCreateRequestBuilder WithStoreId(int storeId)
		{
			StoreId = storeId;
			return this;
		}

		private ProductDto Product { get; set; }
		private int StoreId { get; set; }

		public override ProductCreateRequest Build()
		{
			return new ProductCreateRequest()
				{
					Product = Product,
					StoreId = StoreId
				};
		}
	}
}
