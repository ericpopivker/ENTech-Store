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
	public class StoreDeleteCommandTests : CommandTestsBase<StoreDeleteRequest, StoreDeleteResponse>
	{
		readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();

		[Test]
		public void When_Execute_Then_calls_repository_delete()
		{
			var request = new StoreDeleteRequest
			{
				StoreId = 15
			};

			Command.Execute(request);
			
			_storeRepositoryMock.Verify(x=>x.Delete(It.Is<Entities.StoreModule.Store>(store=>store.Id == request.StoreId)));
		}

		protected override ICommand<StoreDeleteRequest, StoreDeleteResponse> CreateCommand()
		{
			return new StoreDeleteCommand(UnitOfWorkMock.Object, _storeRepositoryMock.Object);
		}
	}
}