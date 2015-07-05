using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreDeleteCommand : DbContextCommandBase<StoreDeleteRequest, StoreDeleteResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _repository;

		public StoreDeleteCommand(IUnitOfWork unitOfWork, IRepository<Entities.StoreModule.Store> repository)
			: base(unitOfWork.DbContext, false)
		{
			_repository = repository;
		}

		public override StoreDeleteResponse Execute(StoreDeleteRequest request)
		{
			var store = _repository.GetById(request.StoreId);

			_repository.Delete(store);

			return new StoreDeleteResponse
			{
				IsSuccess = true
			};
		}

		protected override ArgumentErrorsCollection ValidateInternal(StoreDeleteRequest request)
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
	}
}