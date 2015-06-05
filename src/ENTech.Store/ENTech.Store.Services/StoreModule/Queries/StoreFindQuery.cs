using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Queries
{
	public class StoreFindQuery : IQuery<StoreFindQuery.Criteria, IEnumerable<StoreDto>>
	{
	 	public class Criteria : IQueryCriteria
		{
			public string Name { get; set; }
		}

		public IEnumerable<StoreDto> Execute(IDbContext dbContext, Criteria criteria)
		{
			IQueryable<Entities.StoreModule.Store> result = dbContext.Stores;

			if (criteria != null)
			{
				if (string.IsNullOrWhiteSpace(criteria.Name) == false)
					result = result.Where(x => x.Name.StartsWith(criteria.Name));
			}

			return Mapper.Map<IEnumerable<StoreDto>>(result);
		}
	}
}