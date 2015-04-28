using System;
using System.Linq;
using System.Collections.Generic;
using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductDtoBuilder : BuilderBase<ProductDto,ProductDtoBuilder>
	{
		public ProductDtoBuilder WithName(string name)
		{
			Name = name;
			return this;
		}

		public ProductDtoBuilder WithId(int id)
		{
			Id = id;
			return this;
		}

		public ProductDtoBuilder Inactive()
		{
			IsActive = false;
			return this;
		}

		public ProductDtoBuilder WithCategoryId(int categoryId)
		{
			CategoryId = categoryId;
			return this;
		}

		public ProductDtoBuilder WithOption(ProductOptionDto option)
		{
			if(Options==null)
				Options = new List<ProductOptionDto>();

			if (Options.Any(v => v.Index == option.Index))
				throw new InvalidOperationException("Duplicate option index");

			Options.Add(option);
			return this;
		}

		public ProductDtoBuilder WithVariant(ProductVariantDto variant)
		{

			if (variant.OptionValues != null &&
				variant.OptionValues.Select(o => o.OptionIndex).Except(Options.Select(o => o.Index)).Any())
				throw new InvalidOperationException("Inconsistent options!");

			if (Variants == null)
				Variants = new List<ProductVariantDto>();

			Variants.Add(variant);
			return this;
		}

		public ProductDtoBuilder WithDescription(string description)
		{
			Description = description;
			return this;
		}

		public override ProductDto Build()
		{
			return new ProductDto
				{
					Id = Id,
					IsActive = IsActive,
					Name = Name,
					Options = Options,
					Variants = Variants,
					CategoryId = CategoryId,
					Description = Description
				};
		}

		private string Description { get; set; }

		private string Name { get; set; }

		private int Id { get; set; }

		private bool IsActive { get; set; }

		private List<ProductOptionDto> Options { get; set; }

		private List<ProductVariantDto> Variants { get; set; }

		private int CategoryId { get; set; }
	}
}
