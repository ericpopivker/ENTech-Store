using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductVariantOptionValueDtoBuilder:BuilderBase<ProductVariantOptionValueDto,ProductVariantOptionValueDtoBuilder>
	{
		public ProductVariantOptionValueDtoBuilder WithValue(string value)
		{
			OptionValue = value;
			return this;
		}

		public ProductVariantOptionValueDtoBuilder WithIndex(int index)
		{
			OptionIndex = index;
			return this;
		}

		protected string OptionValue { get; set; }

		protected int OptionIndex { get; set; }

		public override ProductVariantOptionValueDto Build()
		{
			return new ProductVariantOptionValueDto()
				{
					OptionIndex = OptionIndex,
					OptionValue = OptionValue
				};
		}
	}
}
