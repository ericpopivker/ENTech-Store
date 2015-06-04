using ENTech.Store.Entities;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : DbContextCommandBase<StoreFindRequest, StoreFindResponse>
	{
		public StoreFindCommand(IDbContext dbContext, bool requiresTransaction) : base(dbContext, requiresTransaction)
		{
		}

		public override StoreFindResponse Execute(StoreFindRequest request)
		{
			return new StoreFindResponse
			{
				IsSuccess = true
			};
		}
	}
}