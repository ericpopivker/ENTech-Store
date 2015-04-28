using ENTech.Store.Services.Internal.StoreModule.Dtos;
using ENTech.Store.Services.Internal.StoreModule.Requests;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	internal class ProductUpdateRequestBuilder :
		BuilderBase<ProductUpdateRequest, ProductUpdateRequestBuilder>
	{
		public ProductUpdateRequestBuilder WithProduct(ProductDto product)
		{
			Product = product;
			return this;
		}

		public ProductUpdateRequestBuilder WithStoreId(int storeId)
		{
			StoreId = storeId;
			return this;
		}

		private ProductDto Product { get; set; }

		private int StoreId { get; set; }

		public override ProductUpdateRequest Build()
		{
			return new ProductUpdateRequest()
				{
					Product = Product,
					StoreId = StoreId
				};
		}
	}
}
