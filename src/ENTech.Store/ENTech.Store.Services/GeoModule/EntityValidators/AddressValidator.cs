using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.EntityValidators
{
	public class AddressValidator : IAddressValidator
	{
		private IInternalCommandService _internalCommandService;

		public AddressValidator(IInternalCommandService internalCommandService)
		{
			_internalCommandService = internalCommandService;
		}


		public ValidateArgumentResult ValidateId(int addressId)
		{
			var storeGetEntityStateRequest = new AddressGetEntityMetaStateRequest { Id = addressId };
			var response = _internalCommandService.Execute<AddressGetEntityMetaStateRequest, AddressGetEntityMetaStateResponse, AddressGetEntityMetaStateCommand>(storeGetEntityStateRequest);

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
