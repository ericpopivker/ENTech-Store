using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductVariantDtoBuilder : BuilderBase<ProductVariantDto, ProductVariantDtoBuilder>
	{
		public ProductVariantDtoBuilder WithId(int id)
		{
			Id = id;
			return this;
		}

		public ProductVariantDtoBuilder WithPrice(decimal price)
		{
			Price = price;
			return this;
		}

		public ProductVariantDtoBuilder WithSku(string sku)
		{
			Sku = sku;
			return this;
		}

		public ProductVariantDtoBuilder WithQuantity(int quantity)
		{
			QuantityInStock = quantity;
			return this;
		}

		public ProductVariantDtoBuilder WithOptionValue(ProductVariantOptionValueDto optionValue)
		{
			if(OptionValues==null)
				OptionValues = new List<ProductVariantOptionValueDto>();

			if (OptionValues.Any(v => v.OptionIndex == optionValue.OptionIndex))
				throw new InvalidOperationException("Duplicate option index");

			OptionValues.Add(optionValue);

			return this;
		}

		protected string Sku { get; set; }

		protected int Id { get; set; }

		protected decimal Price { get; set; }

		protected int QuantityInStock { get; set; }

		protected List<ProductVariantOptionValueDto> OptionValues { get; set; }

		public override ProductVariantDto Build()
		{
			return new ProductVariantDto()
				{
					Id = Id,
					OptionValues = OptionValues,
					Price = Price,
					QuantityInStock = QuantityInStock,
					Sku = Sku
				};
		}
	}
}
