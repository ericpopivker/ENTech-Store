using ENTech.Store.Services.Internal.StoreModule.Dtos;
using ENTech.Store.Services.Internal.StoreModule.Requests;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductFindRequestBuilder : BuilderBase<ProductFindRequest, ProductFindRequestBuilder>
	{
		public ProductFindRequestBuilder WithStoreId(int id)
		{
			StoreId = id;
			return this;
		}

		public ProductFindRequestBuilder WithCriteria(ProductCriteriaDto productCriteriaDto)
		{
			Criteria = productCriteriaDto;
			return this;
		}

		public override ProductFindRequest Build()
		{
			return new ProductFindRequest
				{
					Criteria = Criteria,
					StoreId = StoreId
				};
		}

		protected int StoreId { get; set; }

		protected ProductCriteriaDto Criteria { get; set; }
	}
}
