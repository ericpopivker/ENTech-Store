using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreUpdateCommand : DbContextCommandBase<StoreUpdateRequest, StoreUpdateResponse>
	{
		private readonly IInternalCommandService _internalCommandService;
		private readonly IRepository<Entities.StoreModule.Store> _repository;

		public StoreUpdateCommand(IUnitOfWork unitOfWork, IInternalCommandService internalCommandService, IRepository<Entities.StoreModule.Store> repository)
			: base(unitOfWork.DbContext, false)
		{
			_internalCommandService = internalCommandService;
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
			var store = _repository.GetById(request.StoreId);

			if (AddressMustBeUpdated(request, store))
			{
				var addressUpdateResponse = _internalCommandService.Execute<AddressUpdateRequest, AddressUpdateResponse, AddressUpdateCommand>(new AddressUpdateRequest
				{
					AddressId = store.AddressId.Value,
					Address = request.Store.Address
				});

				if (addressUpdateResponse.IsSuccess == false)
					return InternalServerError();
			}

			if (AddressMustBeCreated(request, store))
			{
				var addressCreateResponse = _internalCommandService.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(new AddressCreateRequest
				{
					Address = request.Store.Address
				});

				if (addressCreateResponse.IsSuccess == false)
					return InternalServerError();

				store.AddressId = addressCreateResponse.AddressId;
			}


			if (AddressMustBeDeleted(request, store))
			{
				var addressDeleteResponse = _internalCommandService.Execute<AddressDeleteRequest, AddressDeleteResponse, AddressDeleteCommand>(new AddressDeleteRequest
				{
					AddressId = store.AddressId.Value
				});

				if (addressDeleteResponse.IsSuccess == false)
					return InternalServerError();

				store.AddressId = null;
			}
			
			UpdateEntity(request.Store, store);

			_repository.Update(store);

			return new StoreUpdateResponse
			{
				IsSuccess = true
			};
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
	}
}