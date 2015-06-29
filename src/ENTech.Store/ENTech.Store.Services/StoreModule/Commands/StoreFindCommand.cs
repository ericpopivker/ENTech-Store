using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Projections;
using ENTech.Store.Services.StoreModule.Queries;
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
			var query = new StoreFindQuery();
			var result = query.Execute(DbContext, new StoreFindQuery.Criteria
			{
				Name = request.Name
			}).ToList();

			return new StoreFindResponse
			{
				Items = result,
				IsSuccess = true
			};
		}
	}
}