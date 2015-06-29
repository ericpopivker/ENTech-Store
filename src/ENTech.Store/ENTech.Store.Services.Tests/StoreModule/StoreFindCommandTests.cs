using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreFindCommandTests : CommandTestsBase<StoreFindRequest, StoreFindResponse>
	{
		private readonly Mock<IStoreQueryExecuter> _queryExecuterMock = new Mock<IStoreQueryExecuter>();

		[Test]
		public void Execute_When_called_Then_returns_successful_response()
		{
			var request = new StoreFindRequest
			{
				Name = "Name"
			};
			
			var response = Command.Execute(request);
			response.IsSuccess = true;
		}


		protected override ICommand<StoreFindRequest, StoreFindResponse> CreateCommand()
		{
			return new StoreFindCommand(_queryExecuterMock.Object, UnitOfWorkMock.Object);
		}
	}
}