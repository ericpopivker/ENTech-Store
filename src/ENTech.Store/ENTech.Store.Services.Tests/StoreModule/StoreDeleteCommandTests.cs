using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreDeleteCommandTests : CommandTestsBase<StoreDeleteRequest, StoreDeleteResponse>
	{
		readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();
		readonly Mock<IStoreValidator> _storeValidatorMock = new Mock<IStoreValidator>();

		private int _existingStoreId = 15;
		private int _nonexistingStoreId = 16;

		public StoreDeleteCommandTests()
		{
			_storeRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == _existingStoreId)))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id,
					Name = "Store",
					Email = "test@email.gg"
				});

			_storeValidatorMock.Setup(x => x.ValidateId(_nonexistingStoreId))
				.Returns(ValidateArgumentResult.Invalid(new EntityWithIdDoesNotExist("Store")));

			_storeValidatorMock.Setup(x => x.ValidateId(_existingStoreId))
				.Returns(ValidateArgumentResult.Valid());
		}

		protected override void TearDownInternal()
		{
			_storeRepositoryMock.ResetCalls();
			_storeValidatorMock.ResetCalls();
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_get_by_id()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _existingStoreId
			};

			Command.Execute(request);
			
			_storeRepositoryMock.Verify(x=>x.GetById(request.StoreId), Times.Once);
		}

		[Test]
		public void Execute_When_called_Then_calls_repository_delete()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _existingStoreId
			};

			Command.Execute(request);
			
			_storeRepositoryMock.Verify(x=>x.Delete(It.Is<Entities.StoreModule.Store>(store=>store.Id == request.StoreId)));
		}

		[Test]
		public void Validate_When_called_Then_calls_storeValidator_validateId()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _existingStoreId,
				ApiKey = "apiKey"
			};

			Command.Validate(request);

			_storeValidatorMock.Verify(x=>x.ValidateId(_existingStoreId), Times.Once);
		}

		[Test]
		public void Validate_When_called_and_store_does_not_exist_Then_returns_errors()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _nonexistingStoreId,
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsFalse(validationResult.IsValid);
		}
		

		[Test]
		public void Validate_When_called_and_store_exists_Then_returns_no_errors()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _existingStoreId,
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsTrue(validationResult.IsValid);
		}

		protected override ICommand<StoreDeleteRequest, StoreDeleteResponse> CreateCommand()
		{
			return new StoreDeleteCommand(_storeRepositoryMock.Object, _storeValidatorMock.Object, DtoValidatorFactoryMock.Object);
		}
	}
}