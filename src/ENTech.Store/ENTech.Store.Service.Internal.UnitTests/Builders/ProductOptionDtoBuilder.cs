using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductOptionDtoBuilder:BuilderBase<ProductOptionDto,ProductOptionDtoBuilder>
	{
		public ProductOptionDtoBuilder WithId(int id)
		{
			Id = id;
			return this;
		}

		public ProductOptionDtoBuilder WithName(string name)
		{
			Name = name;
			return this;
		}

		public ProductOptionDtoBuilder WithIndex(int index)
		{
			Index = index;
			return this;
		}

		private int Id { get; set; }

		private string Name { get; set; }

		private int Index { get; set; }

		public override ProductOptionDto Build()
		{
			return new ProductOptionDto()
				{
					Id = Id,
					Index = Index,
					Name = Name
				};
		}
	}
}
