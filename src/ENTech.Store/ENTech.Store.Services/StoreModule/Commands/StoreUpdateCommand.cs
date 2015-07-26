using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreUpdateCommand : CommandBase<StoreUpdateRequest, StoreUpdateResponse>
	{
		private readonly IInternalCommandService _internalCommandService;
		private readonly IRepository<Entities.StoreModule.Store> _repository;
		private readonly IStoreValidator _storeValidator;

		public StoreUpdateCommand(IRepository<Entities.StoreModule.Store> repository, IStoreValidator storeValidator,IInternalCommandService internalCommandService, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_internalCommandService = internalCommandService;
			_repository = repository;
			_storeValidator = storeValidator;
		}

		public override StoreUpdateResponse Execute(StoreUpdateRequest request)
		{
			var store = _repository.GetById(request.StoreId);

			if (AddressMustBeUpdated(request, store))
			{
				_internalCommandService.Execute<AddressUpdateRequest, AddressUpdateResponse, AddressUpdateCommand>(new AddressUpdateRequest
				{
					AddressId = store.AddressId.Value,
					Address = request.Store.Address
				});
			}

			if (AddressMustBeCreated(request, store))
			{
				var addressCreateResponse = _internalCommandService.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(new AddressCreateRequest
				{
					Address = request.Store.Address
				});

				store.AddressId = addressCreateResponse.AddressId;
			}


			if (AddressMustBeDeleted(request, store))
			{
				_internalCommandService.Execute<AddressDeleteRequest, AddressDeleteResponse, AddressDeleteCommand>(new AddressDeleteRequest
				{
					AddressId = store.AddressId.Value
				});


				store.AddressId = null;
			}
			
			UpdateEntity(request.Store, store);

			_repository.Update(store);

			return new StoreUpdateResponse();
		}

		private static bool AddressMustBeDeleted(StoreUpdateRequest request, Entities.StoreModule.Store store)
		{
			return request.Store.Address == null && store.AddressId.HasValue;
		}

		private static bool AddressMustBeCreated(StoreUpdateRequest request, Entities.StoreModule.Store store)
		{
			return request.Store.Address != null && store.AddressId.HasValue == false;
		}

		private static bool AddressMustBeUpdated(StoreUpdateRequest request, Entities.StoreModule.Store store)
		{
			return request.Store.Address != null && store.AddressId.HasValue;
		}

		private void UpdateEntity(StoreUpdateDto source, Entities.StoreModule.Store target)
		{
			target.Email = source.Email;
			target.Logo = source.Logo;
			target.Name = source.Name;
			target.Phone = source.Phone;
			target.TimezoneId = source.TimezoneId;
		}

		protected override void ValidateRequestInternal(StoreUpdateRequest request, ValidateRequestResult<StoreUpdateRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.StoreId))
			{
				var result = _storeValidator.ValidateId(request.StoreId);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.StoreId, result.ArgumentError);
			}
		}
	}
}