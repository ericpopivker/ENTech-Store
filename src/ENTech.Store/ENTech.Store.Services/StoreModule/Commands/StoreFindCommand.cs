using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Queries;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : DbContextCommandBase<StoreFindRequest, StoreFindResponse>
	{
		public StoreFindCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
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
			};
		}
	}
}