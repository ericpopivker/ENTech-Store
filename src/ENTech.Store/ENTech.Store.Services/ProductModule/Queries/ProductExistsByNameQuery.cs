using System.Linq;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Services.ProductModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Queries
{
	public class ProductExiststByNameQuery : IQuery<ProductExiststByNameQuery.Criteria, bool>
	{
	 	public class Criteria : IQueryCriteria
		{
			public string Name { get; set; }	
		}

		public bool Execute(IDbContext dbContext, Criteria criteria)
		{
			var result = dbContext.Products.Any(x => x.Name == criteria.Name);
			return result;
		}
	}
}