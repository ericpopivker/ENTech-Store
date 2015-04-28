using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class ProductCriteriaDtoBuilder : BuilderBase<ProductCriteriaDto, ProductCriteriaDtoBuilder>
	{
		public ProductCriteriaDtoBuilder WithCategoryId(int id)
		{
			CategoryId = id;
			return this;
		}

		public ProductCriteriaDtoBuilder WithPageSize(int size)
		{
			PageSize = size;
			return this;
		}

		public ProductCriteriaDtoBuilder WithPageIndex(int index)
		{
			PageIndex = index;
			return this;
		}

		public ProductCriteriaDtoBuilder WithSortField1(SortFieldDto sortFieldDto)
		{
			SortField1 = sortFieldDto;
			return this;
		}

		public override ProductCriteriaDto Build()
		{
			return new ProductCriteriaDto
				{
					CategoryId = CategoryId,
					PageSize = PageSize??10,
					PageIndex = PageIndex??1,
					SortField1 = SortField1
				};
		}

		protected int CategoryId { get; set; }
		protected int? PageSize { get; set; }
		protected int? PageIndex { get; set; }

		protected SortFieldDto SortField1 { get; set; }
	}
}
