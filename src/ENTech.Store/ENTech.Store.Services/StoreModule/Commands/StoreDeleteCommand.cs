using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreDeleteCommand : CommandBase<StoreDeleteRequest, StoreDeleteResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _repository;
		private readonly IStoreValidator _storeValidator;

		public StoreDeleteCommand(IRepository<Entities.StoreModule.Store> repository, IStoreValidator storeValidator, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_repository = repository;
			_storeValidator = storeValidator;
		}

		protected override void ValidateRequestInternal(StoreDeleteRequest request, ValidateRequestResult<StoreDeleteRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.StoreId))
			{
				var result = _storeValidator.ValidateId(request.StoreId);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.StoreId, result.ArgumentError);
			}
		}

		public override StoreDeleteResponse Execute(StoreDeleteRequest request)
		{
			var store = _repository.GetById(request.StoreId);

			_repository.Delete(store);

			return new StoreDeleteResponse();
		}
	}
}