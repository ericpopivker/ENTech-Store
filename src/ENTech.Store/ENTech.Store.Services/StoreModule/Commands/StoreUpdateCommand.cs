using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreUpdateCommand : DbContextCommandBase<StoreUpdateRequest, StoreUpdateResponse>
	{
		public StoreUpdateCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override StoreUpdateResponse Execute(StoreUpdateRequest request)
		{
			return new StoreUpdateResponse
			{
				IsSuccess = true
			};
		}
	}
}