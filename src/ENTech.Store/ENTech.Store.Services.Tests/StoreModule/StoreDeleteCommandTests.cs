using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
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
		}

		protected override void TearDownInternal()
		{
			_storeRepositoryMock.ResetCalls();
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
		public void Validate_When_called_Then_calls_repository_get_by_id()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = _existingStoreId,
				ApiKey = "apiKey"
			};

			Command.Validate(request);

			_storeRepositoryMock.Verify(x => x.GetById(request.StoreId));
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

			Assert.IsNotEmpty(validationResult);
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

			Assert.IsEmpty(validationResult);
		}

		protected override ICommand<StoreDeleteRequest, StoreDeleteResponse> CreateCommand()
		{
			return new StoreDeleteCommand(_storeRepositoryMock.Object);
		}
	}
}