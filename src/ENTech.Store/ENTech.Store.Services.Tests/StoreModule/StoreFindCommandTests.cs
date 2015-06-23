using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreFindCommandTests : CommandTestsBase<StoreFindRequest, StoreFindResponse>
	{
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
			return new StoreFindCommand(UnitOfWorkMock.Object);
		}
	}
}