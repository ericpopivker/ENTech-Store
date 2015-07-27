using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.StoreModule.Requests;

namespace ENTech.Store.Services.ProductModule.Validators.EntityValidators
{
	public class StoreValidator : IStoreValidator
	{
		private IInternalCommandService _internalCommandService;

		public StoreValidator(IInternalCommandService internalCommandService)
		{
			_internalCommandService = internalCommandService;
		}


		public ValidateArgumentResult ValidateId(int storeId)
		{
			var storeGetEntityStateRequest = new StoreGetEntityMetaStateRequest{ Id = storeId };
			var response = _internalCommandService.Execute(storeGetEntityStateRequest);

			if (response.EntityMetaState == EntityMetaState.NotFound)
			{
				var error = new EntityWithIdDoesNotExist("Store");
				return ValidateArgumentResult.Invalid(error);
			}

			if (response.EntityMetaState == EntityMetaState.Deleted)
			{
				var error = new EntityWithIdIsDeletedArgumentError("Store");
				return ValidateArgumentResult.Invalid(error);
			}


			return ValidateArgumentResult.Valid();
		}

	}
}
