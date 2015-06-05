using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : DbContextCommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		public StoreGetByIdCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			return new StoreGetByIdResponse
			{
				IsSuccess = true,
				Item = new StoreDto
				{

				}
			};
		}
	}
}