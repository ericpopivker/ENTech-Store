using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Queries;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : DbContextCommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		public StoreGetByIdCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var query = new StoreGetByIdQuery();
			var result = query.Execute(DbContext, new StoreGetByIdQuery.Criteria
			{
				Id = request.Id.Value
			});
			return new StoreGetByIdResponse
			{
				Item = result
			};
		}
	}
}