using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Extensions;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : DbContextCommandBase<StoreFindRequest, StoreFindResponse>
	{
		private readonly IStoreQueryExecuter _queryExecuter;

		public StoreFindCommand(IStoreQueryExecuter queryExecuter, IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
			_queryExecuter = queryExecuter;
		}

		public override StoreFindResponse Execute(StoreFindRequest request)
		{
			var result = _queryExecuter.Find(new StoreFindCriteria
			{
				Name = request.Criteria.Decode(x => x.Name),
				Paging = new PagingOptions
				{
					PageSize = request.Criteria.PagingOptions.Decode(x=>x.PageSize),
					PageIndex = request.Criteria.PagingOptions.Decode(x=>x.PageIndex)
				}
			});

			return new StoreFindResponse
			{
				Items = result.Select(x=>new StoreDto()).ToList(),
				IsSuccess = true
			};
		}
	}
}