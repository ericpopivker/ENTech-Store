using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Entities.ProductModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Queries
{
	public class ProductFindQuery : IQuery<ProductFindQuery.Criteria, IEnumerable<ProductDto>>
	{
	 	public class Criteria : IQueryCriteria
		{
			public string Name { get; set; }
			public int? StoreId { get; set; }
		}

		public IEnumerable<ProductDto> Execute(IDbContext dbContext, Criteria criteria)
		{
			IQueryable<Product> result = dbContext.Products;

			if (criteria != null)
			{
				if (string.IsNullOrWhiteSpace(criteria.Name) == false)
					result = result.Where(x => x.Name.StartsWith(criteria.Name));

				if (criteria.StoreId.HasValue)
					result = result.Where(x => x.Store.Id == criteria.StoreId);
			}

			return Mapper.Map<IEnumerable<ProductDto>>(result);
		}
	}
}