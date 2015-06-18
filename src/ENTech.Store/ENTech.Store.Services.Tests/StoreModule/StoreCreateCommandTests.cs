using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
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
		readonly Mock<IRepository<Country>> _countryRepositoryMock = new Mock<IRepository<Country>>();
		readonly Mock<IRepository<State>> _stateRepositoryMock = new Mock<IRepository<State>>();

		public StoreCreateCommandTests()
		{
			_countryRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => new Country
			{
				Id = id,
				Name = string.Format("test country {0}", id),
				Code = string.Format("1000{0}", id)
			});

			_stateRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => new State
			{
				Id = id,
				Name = string.Format("test country {0}", id),
				Code = string.Format("1000{0}", id)
			});
		}

		[SetUp]
		public void SetUp()
		{
			_storeRepositoryMock.ResetCalls();
			_countryRepositoryMock.ResetCalls();
			_stateRepositoryMock.ResetCalls();
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_create()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object, 
				_storeRepositoryMock.Object,
				_countryRepositoryMock.Object,
				_stateRepositoryMock.Object);

			var storeCreateDto = new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = new AddressCreateDto
				{
					CountryId = 1,
					StateId = 1,
					Street = "Street 1",
					Street2 = "Street 2",
					Zip = "12345",
					City = "City 17"
				},
				Phone = "1231231234",
				Timezone = "Eastern"
			};

			var request = new StoreCreateRequest
			{
				Store = storeCreateDto
			};

			command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.IsAny<Entities.StoreModule.Store>()),
				Times.Once);
		}

		[Test]
		public void Execute_When_called_with_filled_fields_Then_calls_repository_create_with_these_fields_transferred()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object, 
				_storeRepositoryMock.Object,
				_countryRepositoryMock.Object,
				_stateRepositoryMock.Object);

			var storeCreateDto = new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = new AddressCreateDto
				{
					CountryId = 1,
					StateId = 1,
					Street = "Street 1",
					Street2 = "Street 2",
					Zip = "12345",
					City = "City 17"
				},
				Phone = "1231231234",
				Timezone = "Eastern"
			};

			var request = new StoreCreateRequest
			{
				Store = storeCreateDto
			};

			command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.Is<Entities.StoreModule.Store>(y =>
				y.Name == storeCreateDto.Name &&
				y.Email == storeCreateDto.Email &&
				y.Logo == storeCreateDto.Logo &&
				y.Phone == storeCreateDto.Phone &&
				y.TimezoneId == storeCreateDto.Timezone &&
				y.Address != null &&
				y.Address.Street == storeCreateDto.Address.Street &&
				y.Address.Street2 == storeCreateDto.Address.Street2 &&
				y.Address.City == storeCreateDto.Address.City &&
				y.Address.State != null &&
				y.Address.State.Id == storeCreateDto.Address.StateId &&
				y.Address.StateOther == null &&
				y.Address.Country != null &&
				y.Address.Country.Id == storeCreateDto.Address.CountryId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_filled_country_id_Then_calls_country_repository_with_that_country_id()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object,
				_storeRepositoryMock.Object,
				_countryRepositoryMock.Object,
				_stateRepositoryMock.Object);

			var storeCreateDto = new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = new AddressCreateDto
				{
					CountryId = 2,
					StateId = 9,
					Street = "Street 1",
					Street2 = "Street 2",
					Zip = "12345",
					City = "City 17"
				},
				Phone = "1231231234",
				Timezone = "Eastern"
			};

			var request = new StoreCreateRequest
			{
				Store = storeCreateDto
			};

			command.Execute(request);

			_countryRepositoryMock.Verify(x=>x.GetById(storeCreateDto.Address.CountryId), Times.Once);
			_countryRepositoryMock.Verify(x => x.GetById(It.Is<int>(y => y != storeCreateDto.Address.CountryId)), Times.Never);
		}

		[Test]
		public void Execute_When_called_with_filled_state_id_Then_calls_state_repository_with_that_state_id()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object,
				_storeRepositoryMock.Object,
				_countryRepositoryMock.Object,
				_stateRepositoryMock.Object);

			var storeCreateDto = new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = new AddressCreateDto
				{
					CountryId = 2,
					StateId = 9,
					Street = "Street 1",
					Street2 = "Street 2",
					Zip = "12345",
					City = "City 17"
				},
				Phone = "1231231234",
				Timezone = "Eastern"
			};

			var request = new StoreCreateRequest
			{
				Store = storeCreateDto
			};

			command.Execute(request);

			_stateRepositoryMock.Verify(x => x.GetById(storeCreateDto.Address.StateId.Value), Times.Once);
			_stateRepositoryMock.Verify(x => x.GetById(It.Is<int>(y => y != storeCreateDto.Address.StateId.Value)), Times.Never);
		}

		[Test]
		public void Execute_When_called_with_filled_state_other_Then_does_not_call_state_repository()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object,
				_storeRepositoryMock.Object,
				_countryRepositoryMock.Object,
				_stateRepositoryMock.Object);

			var storeCreateDto = new StoreCreateDto
			{
				Name = "test store name",
				Email = "test@email.gg",
				Logo = "logo.jpg",
				Address = new AddressCreateDto
				{
					CountryId = 2,
					StateOther = "hello",
					Street = "Street 1",
					Street2 = "Street 2",
					Zip = "12345",
					City = "City 17"
				},
				Phone = "1231231234",
				Timezone = "Eastern"
			};

			var request = new StoreCreateRequest
			{
				Store = storeCreateDto
			};

			command.Execute(request);

			_stateRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
		}
	}
}