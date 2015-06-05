using System.Linq;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Queries
{
	public class StoreGetByIdQuery : IQuery<StoreGetByIdQuery.Criteria, StoreDto>
	{
	 	public class Criteria : IQueryCriteria
		{
			public int Id { get; set; }	
		}

		public StoreDto Execute(IDbContext dbContext, Criteria criteria)
		{
			var result = dbContext.Stores.FirstOrDefault(x => x.Id == criteria.Id);
			return Mapper.Map<StoreDto>(result);
		}
	}
}