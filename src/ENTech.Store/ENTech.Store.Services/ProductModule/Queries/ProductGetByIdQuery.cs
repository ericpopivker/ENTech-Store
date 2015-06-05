using System.Linq;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Queries
{
	public class ProductGetByIdQuery : IQuery<ProductGetByIdQuery.Criteria, ProductDto>
	{
	 	public class Criteria : IQueryCriteria
		{
			public int Id { get; set; }	
		}

		public ProductDto Execute(IDbContext dbContext, Criteria criteria)
		{
			var result = dbContext.Products.FirstOrDefault(x => x.Id == criteria.Id);
			return Mapper.Map<ProductDto>(result);
		}
	}
}