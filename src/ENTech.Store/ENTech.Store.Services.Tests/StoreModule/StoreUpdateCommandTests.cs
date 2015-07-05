using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Commands;
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

		private int _existingStoreId = 15;
		private int _nonexistingStoreId = 16;

		public StoreUpdateCommandTests()
		{
			_storeRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == _existingStoreId)))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id,
					Name = "Store",
					Email = "test@email.gg"
				});
		}

		[Test]
		public void When_Validate_Then_calls_repository_get_by_id()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _existingStoreId,
				ApiKey = "apiKey"
			};

			Command.Validate(request);

			_storeRepositoryMock.Verify(x => x.GetById(request.StoreId));
		}

		[Test]
		public void When_Validate_and_store_does_not_exist_Then_returns_errors()
		{
			var request = new StoreUpdateRequest
			{
				StoreId = _nonexistingStoreId,
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsNotEmpty(validationResult);
		}


		[Test]
		public void When_Validate_and_store_exists_Then_returns_no_errors()
		{
			var request = new StoreUpdateRequest()
			{
				StoreId = _existingStoreId,
				ApiKey = "apiKey"
			};

			var validationResult = Command.Validate(request);

			Assert.IsEmpty(validationResult);
		}

		protected override ICommand<StoreUpdateRequest, StoreUpdateResponse> CreateCommand()
		{
			return new StoreUpdateCommand(UnitOfWorkMock.Object, _storeRepositoryMock.Object);
		}
	}
}