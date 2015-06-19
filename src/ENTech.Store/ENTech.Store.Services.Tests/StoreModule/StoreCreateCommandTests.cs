using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreCreateCommandTests
	{
		readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();
		readonly Mock<IInternalCommandService> _internalCommandServiceMock = new Mock<IInternalCommandService>();
		readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
		
		
		private static int _validCountryId = 99;
		private static int _invalidCountryId = 501;
		private int _responseAddressId = 12345645;


		public StoreCreateCommandTests()
		{
			_mapperMock.Setup(x => x.Map<AddressDto, AddressCreateDto>(It.IsAny<AddressDto>()))
				.Returns((AddressDto originalDto) => new AddressCreateDto
				{
					StateId = originalDto.StateId,
					Street2 = originalDto.Street2,
					StateOther = originalDto.StateOther,
					Zip = originalDto.Zip,
					Street = originalDto.Street,
					City = originalDto.City,
					CountryId = originalDto.CountryId
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(
						It.Is<AddressCreateRequest>(req => req.Address != null && req.Address.CountryId == _validCountryId)))
				.Returns(new AddressCreateResponse
				{
					AddressId = _responseAddressId,
					IsSuccess = true
				});

			_internalCommandServiceMock.Setup(
				x =>
					x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(
						It.Is<AddressCreateRequest>(req => req.Address != null && req.Address.CountryId == _invalidCountryId)))
				.Returns(new AddressCreateResponse
				{
					IsSuccess = false,
					ArgumentErrors =
						new ArgumentErrorsCollection()
						{
							new ArgumentError {ArgumentName = "test error name", ErrorCode = 500, ErrorMessage = "Message"}
						}
				});

			RequestValidatorErrorMessagesDictionary.RegisterAll();
		}

		[SetUp]
		public void SetUp()
		{
			_storeRepositoryMock.ResetCalls();
			_internalCommandServiceMock.ResetCalls();
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_create()
		{
			var request = GetStoreCreateRequest(GetValidAddressDto());

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.IsAny<Entities.StoreModule.Store>()),
				Times.Once);
		}
		[Test]
		public void Execute_When_called_with_filled_fields_Then_calls_repository_create_with_these_fields_transferred()
		{
			var request = GetStoreCreateRequest(GetValidAddressDto());

			var storeCreateDto = request.Store;

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.Is<Entities.StoreModule.Store>(y =>
				y.Name == storeCreateDto.Name &&
				y.Email == storeCreateDto.Email &&
				y.Logo == storeCreateDto.Logo &&
				y.Phone == storeCreateDto.Phone &&
				y.TimezoneId == storeCreateDto.Timezone &&
				y.AddressId != null)), Times.Once);
		}

		[Test]
		public void Execute_When_called_Then_calls_AddressCreateCommand_through_internal_command_service_to_create_address()
		{
			var request = GetStoreCreateRequest(GetValidAddressDto());

			Command.Execute(request);

			_internalCommandServiceMock.Verify(x=>x.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(It.IsAny<AddressCreateRequest>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_valid_address_Then_saves_received_address_id_to_repository()
		{
			var request = GetStoreCreateRequest(GetValidAddressDto());

			var storeCreateDto = request.Store;

			Command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.Is<Entities.StoreModule.Store>(y =>
				y.Name == storeCreateDto.Name &&
				y.Email == storeCreateDto.Email &&
				y.Logo == storeCreateDto.Logo &&
				y.Phone == storeCreateDto.Phone &&
				y.TimezoneId == storeCreateDto.Timezone &&
				y.AddressId == _responseAddressId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_invalid_address_Then_returns_failed_response()
		{
			var request = GetStoreCreateRequest(GetInvalidAddressDto());

			var response = Command.Execute(request);

			Assert.IsFalse(response.IsSuccess);
		}

		[Test]
		public void Execute_When_called_with_invalid_address_Then_returns_internal_error_response()
		{
			var request = GetStoreCreateRequest(GetInvalidAddressDto());

			var response = Command.Execute(request);

			Assert.AreEqual(CommonErrorCode.InternalServerError, response.Error.ErrorCode);
		}



		private static StoreCreateRequest GetStoreCreateRequest(AddressDto addressDto)
		{
			return new StoreCreateRequest
			{
				Store = GetStoreCreateDto(addressDto)
			};
		}

		private static StoreCreateDto GetStoreCreateDto(AddressDto address)
		{
			return new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = address,
				Phone = "1231231234",
				Timezone = "Eastern"
			};
		}

		private static AddressDto GetInvalidAddressDto()
		{
			return new AddressDto
			{
				CountryId = _invalidCountryId,
				StateId = 9,
				Street = "Street 1",
				Street2 = "Street 2",
				Zip = "12345",
				City = "City 17"
			};
		}

		private static AddressDto GetValidAddressDto()
		{
			return new AddressDto
			{
				CountryId = _validCountryId,
				StateId = 9,
				Street = "Street 1",
				Street2 = "Street 2",
				Zip = "12345",
				City = "City 17"
			};
		}

		private StoreCreateCommand Command
		{
			get
			{
				return new StoreCreateCommand(_unitOfWorkMock.Object,
					_storeRepositoryMock.Object, _internalCommandServiceMock.Object, _mapperMock.Object);
			}
		}
	}
}