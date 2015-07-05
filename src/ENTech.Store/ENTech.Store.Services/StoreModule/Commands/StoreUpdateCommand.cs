using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreUpdateCommand : DbContextCommandBase<StoreUpdateRequest, StoreUpdateResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _repository;

		public StoreUpdateCommand(IUnitOfWork unitOfWork, IRepository<Entities.StoreModule.Store> repository)
			: base(unitOfWork.DbContext, false)
		{
			_repository = repository;
		}

		protected override ArgumentErrorsCollection ValidateInternal(StoreUpdateRequest request)
		{
			var result = new ArgumentErrorsCollection();

			var store = _repository.GetById(request.StoreId);

			if (store == null)
				result["StoreId"] = new ArgumentError
				{
					ArgumentName = "StoreId",
					ErrorCode = CommonErrorCode.ArgumentErrors,
					ErrorMessage = "Store does not exist"
				};

			return result;
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