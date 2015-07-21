using System;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.Commands;
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
	public class StoreGetByIdCommandTests : CommandTestsBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();

		private const int StoreWithoutAddressId = 423;
		private const int StoreWithAddressId = 15;
		private const int StoreWithIncorrectAddressId = 16;
		private const int StoreAddressId = 43;
		private const int IncorrectAddressId = 55555;

		protected override void TearDownInternal()
		{
			_storeRepositoryMock.ResetCalls();
			InternalCommandServiceMock.ResetCalls();
			MapperMock.ResetCalls();
		}

		public StoreGetByIdCommandTests()
		{
			_storeRepositoryMock.Setup(x => x.GetById(StoreWithoutAddressId))
				.Returns(new Entities.StoreModule.Store
				{
					Id = StoreWithoutAddressId,
					Name = "Test",
					Phone = "1231231234",
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddDays(-1),
					Email = "test@email.gg",
					Logo = "logo.jpg",
					TimezoneId = "testtest"
				});

			_storeRepositoryMock.Setup(x => x.GetById(StoreWithIncorrectAddressId))
				.Returns(new Entities.StoreModule.Store
				{
					Id = StoreWithIncorrectAddressId,
					Name = "Test-With-Address",
					Phone = "1231231234",
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddDays(-1),
					Email = "test@email.gg",
					Logo = "logo.jpg",
					TimezoneId = "testtest1",
					AddressId = IncorrectAddressId
				});

			_storeRepositoryMock.Setup(x => x.GetById(StoreWithAddressId))
				.Returns(new Entities.StoreModule.Store
				{
					Id = StoreWithAddressId,
					Name = "Test-With-Address",
					Phone = "1231231234",
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddDays(-1),
					Email = "test@email.gg",
					Logo = "logo.jpg",
					TimezoneId = "testtest1",
					AddressId = StoreAddressId
				});

			InternalCommandServiceMock.Setup(x => x.Execute<AddressGetByIdRequest, AddressGetByIdResponse, AddressGetByIdCommand>(It.Is<AddressGetByIdRequest>(y => y.Id == StoreAddressId))).Returns(new AddressGetByIdResponse
			{
				IsSuccess = true,
				Item = new GeoModule.Dtos.AddressDto
				{
					Street2 = "Street 2",
					StateOther = "STA",
					Zip = "12345",
					Street = "Street",
					StateId = 1,
					CountryId = 1,
					City = "City"
				}
			});
			
			InternalCommandServiceMock.Setup(x => x.Execute<AddressGetByIdRequest, AddressGetByIdResponse, AddressGetByIdCommand>(It.Is<AddressGetByIdRequest>(y => y.Id == IncorrectAddressId))).Returns(new AddressGetByIdResponse
			{
				IsSuccess = false,
				Error = new Error()
			});

			MapperMock.Setup(x => x.Map<Entities.StoreModule.Store, StoreDto>(It.IsAny<Entities.StoreModule.Store>()))
				.Returns((Entities.StoreModule.Store store) => new StoreDto
				{
					Id = store.Id,
					Address = null,
					Name = store.Name,
					Logo = store.Logo,
					Email = store.Email,
					Phone = store.Phone
				});

			MapperMock.Setup(x => x.Map<GeoModule.Dtos.AddressDto, AddressDto>(It.IsAny<GeoModule.Dtos.AddressDto>()))
				.Returns((GeoModule.Dtos.AddressDto address) => new AddressDto
				{
					StateOther = address.StateOther,
					Street2 = address.Street2,
					City = address.City,
					Zip = address.Zip,
					Street = address.Street,
					StateId = address.StateId,
					CountryId = address.CountryId
				});

			RequestValidatorErrorMessagesDictionary.RegisterAll();
		}

		[Test]
		public void Execute_When_called_Then_calls_for_query_provider()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_Then_passes_correct_criteria_type_to_query_executer()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_concrete_id_Then_passes_that_id_into_criteria()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.Is<int>(id => id == StoreWithoutAddressId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_Then_uses_mapper_to_map_project()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			MapperMock.Verify(mock => mock.Map<Entities.StoreModule.Store, StoreDto>(It.Is<Entities.StoreModule.Store>(proj => proj.Id == StoreWithoutAddressId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_for_store_without_address_Then_returns_storeDto_with_null_address()
		{
			var result = Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			Assert.IsNull(result.Item.Address);
		}

		[Test]
		public void Execute_When_called_for_store_without_address_Then_does_not_call_internal_command_service_execute_for_addressGetByIdCommand()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithoutAddressId
			});

			InternalCommandServiceMock.Verify(x=>x.Execute<AddressGetByIdRequest, AddressGetByIdResponse, AddressGetByIdCommand>(It.IsAny<AddressGetByIdRequest>()), Times.Never);
		}

		[Test]
		public void Execute_When_called_for_store_with_address_Then_does_not_call_internal_command_service_execute_for_addressGetByIdCommand()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithAddressId
			});

			InternalCommandServiceMock.Verify(x=>x.Execute<AddressGetByIdRequest, AddressGetByIdResponse, AddressGetByIdCommand>(It.Is<AddressGetByIdRequest>(y=>y.Id == StoreAddressId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_for_store_that_has_address_Then_calls_addressGetByIdCommand()
		{
			var result = Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithAddressId
			});

			Assert.IsNotNull(result.Item.Address);
		}

		[Test]
		public void Execute_When_called_for_store_that_has_invalid_addressId_Then_returns_internal_server_error()
		{
			var result = Command.Execute(new StoreGetByIdRequest
			{
				Id = StoreWithIncorrectAddressId
			});

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(CommonErrorCode.InternalServerError, result.Error.ErrorCode);
		}

		protected override ICommand<StoreGetByIdRequest, StoreGetByIdResponse> CreateCommand()
		{
			return new StoreGetByIdCommand(_storeRepositoryMock.Object, MapperMock.Object, InternalCommandServiceMock.Object);
		}
	}
}