using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreUpdateCommandTests : CommandTestsBase<StoreUpdateRequest, StoreUpdateResponse>
	{
		readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();
		readonly Mock<IInternalCommandService> _internalCommandServiceMock = new Mock<IInternalCommandService>();

		private int _existingStoreIdWithoutAddressId = 14;
		private int _existingStoreIdWithAddressId = 15;
		private int _existingStoreIdWithAddressIdThatCannotBeDeleted = 16;
		private int _nonexistingStoreId = 17;
		
		private int _originalAddressId = 123;
		private int _createdAddressId = 124;
		private int _originalAddressIdThatFailsToBeDeleted = 125;

		private int _validCountryId = 1;
		private int _invalidCountryId = 2;

		protected override void TearDownInternal()
		{
			_internalCommandServiceMock.ResetCalls();
			_storeRepositoryMock.ResetCalls();
		}
		
		public StoreUpdateCommandTests()
		{
			_storeRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == _existingStoreIdWithAddressId)))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id,
					Name = "Store",
					Email = "test@email.gg",
					AddressId = _originalAddressId
				});

			_storeRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == _existingStoreIdWithoutAddressId)))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id,
					Name = "Store",
					Email = "test@email.gg"
				});

			_storeRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == _existingStoreIdWithAddressIdThatCannotBeDeleted)))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id,
					Name = "Store",
					Email = "test@email.gg",
					AddressId = _originalAddressIdThatFailsToBeDeleted
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(
						It.Is<AddressCreateRequest>(req => req.Address.CountryId == _validCountryId)))
				.Returns(new AddressCreateResponse
				{
					AddressId = _createdAddressId,
					IsSuccess = true
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(
						It.Is<AddressCreateRequest>(req => req.Address.CountryId == _invalidCountryId)))
				.Returns(new AddressCreateResponse
				{
					IsSuccess = false
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressUpdateRequest, AddressUpdateResponse, AddressUpdateCommand>(
						It.Is<AddressUpdateRequest>(req => req.Address.CountryId == _validCountryId)))
				.Returns(new AddressUpdateResponse
				{
					IsSuccess = true
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressUpdateRequest, AddressUpdateResponse, AddressUpdateCommand>(
						It.Is<AddressUpdateRequest>(req => req.Address.CountryId == _invalidCountryId)))
				.Returns(new AddressUpdateResponse
				{
					IsSuccess = false
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressDeleteRequest, AddressDeleteResponse, AddressDeleteCommand>(
						It.Is<AddressDeleteRequest>(req => req.AddressId == _originalAddressId)))
				.Returns(new AddressDeleteResponse
				{
					IsSuccess = true
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressDeleteRequest, AddressDeleteResponse, AddressDeleteCommand>(
						It.Is<AddressDeleteRequest>(req => req.AddressId == _originalAddressIdThatFailsToBeDeleted)))
				.Returns(new AddressDeleteResponse
				{
					IsSuccess = false
				});

			RequestValidatorErrorMessagesDictionary.RegisterAll();
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_get_by_id()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.GetById(request.StoreId));
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_update()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Update(It.IsAny<Entities.StoreModule.Store>()));
		}

		[Test]
		public void Execute_When_called_for_store_with_address_Then_calls_internal_command_service_address_update()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_internalCommandServiceMock.Verify(
				x =>
					x.Execute<AddressUpdateRequest, AddressUpdateResponse, AddressUpdateCommand>(
						It.Is<AddressUpdateRequest>(r => r.AddressId == _originalAddressId && r.Address == request.Store.Address)));
		}

		[Test]
		public void Execute_When_called_for_store_without_address_Then_calls_internal_command_service_address_create()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithoutAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_internalCommandServiceMock.Verify(
				x =>
					x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(
						It.Is<AddressCreateRequest>(r => r.Address == request.Store.Address)));
		}

		[Test]
		public void Execute_When_called_for_store_without_address_Then_sets_returned_from_createAddressCommand_for_store()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithoutAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Update(It.Is<Entities.StoreModule.Store>(
				store => store.AddressId == _createdAddressId)));
		}

		[Test]
		public void Execute_When_called_for_store_without_address_and_get_failed_response_from_createAddressCommand_Then_returns_internal_server_error()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithoutAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_invalidCountryId))
			};

			var response = Command.Execute(request);

			Assert.AreEqual(CommonErrorCode.InternalServerError , response.Error.ErrorCode);
		}

		[Test]
		public void Execute_When_called_for_store_with_address_and_get_failed_response_from_updateAddressCommand_Then_returns_internal_server_error()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_invalidCountryId))
			};

			var response = Command.Execute(request);

			Assert.AreEqual(CommonErrorCode.InternalServerError , response.Error.ErrorCode);
		}

		[Test]
		public void Execute_When_called_for_store_with_address_and_dto_has_no_address_Then_calls_internalCommandService_for_addressDeleteCommand()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(null)
			};

			Command.Execute(request);

			_internalCommandServiceMock.Verify(
				x =>
					x.Execute<AddressDeleteRequest, AddressDeleteResponse, AddressDeleteCommand>(
						It.Is<AddressDeleteRequest>(r => r.AddressId == _originalAddressId)));
		}

		[Test]
		public void Execute_When_called_for_store_with_address_and_dto_has_no_address_and_addressDeleteCommand_fails_Then_returns_internal_server_error()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressIdThatCannotBeDeleted,
				Store = GetStoreUpdateDto(null)
			};

			var response = Command.Execute(request);

			Assert.AreEqual(CommonErrorCode.InternalServerError, response.Error.ErrorCode);
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_update_with_new_data()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Update(It.Is<Entities.StoreModule.Store>(
				store => store.Id == _existingStoreIdWithAddressId
				         && store.Name == request.Store.Name
						 && store.Email == request.Store.Email
						 && store.Phone == request.Store.Phone
						 && store.AddressId == _originalAddressId
						 && store.Logo == request.Store.Logo
						 && store.TimezoneId == request.Store.TimezoneId
				)));
		}

		[Test]
		public void Validate_When_called_Then_calls_repository_get_by_id()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				ApiKey = "apiKey",
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			Command.Validate(request);

			_storeRepositoryMock.Verify(x => x.GetById(request.StoreId));
		}

		[Test]
		public void Validate_When_called_and_store_does_not_exist_Then_returns_errors()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _nonexistingStoreId,
				ApiKey = "apiKey",
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId))
			};

			var validationResult = Command.Validate(request);

			Assert.IsNotEmpty(validationResult);
		}

		[Test]
		public void Validate_When_called_and_store_dto_is_null_Then_returns_errors()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreIdWithAddressId,
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsNotEmpty(validationResult);
		}


		[Test]
		public void Validate_When_called_and_store_exists_Then_returns_no_errors()
		{
			var request = new StoreUpdateRequest()
			{
				StoreId = _existingStoreIdWithAddressId,
				Store = GetStoreUpdateDto(WithAddressDto(_validCountryId)),
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsEmpty(validationResult);
		}

		private StoreUpdateDto GetStoreUpdateDto(AddressCreateOrUpdateDto address)
		{
			return new StoreUpdateDto
			{
				Name = "new-name",
				Email = "new-email@email.gg",
				Phone = "1231231234",
				Logo = "logo.jpg",
				Address = address
			};
		}

		private AddressCreateOrUpdateDto WithAddressDto(int countryId)
		{
			return new AddressCreateOrUpdateDto
			{
				StateId = 3,
				Street2 = "Street2",
				Zip = "33322",
				Street = "Street",
				City = "City",
				CountryId = countryId
			};
		}

		protected override ICommand<StoreUpdateRequest, StoreUpdateResponse> CreateCommand()
		{
			return new StoreUpdateCommand(UnitOfWorkMock.Object, _internalCommandServiceMock.Object, _storeRepositoryMock.Object);
		}
	}
}