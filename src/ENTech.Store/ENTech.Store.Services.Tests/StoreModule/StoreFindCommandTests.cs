using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Dtos;
using ENTech.Store.Services.StoreModule;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
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
		public void Execute_When_called_with_valid_criteria_Then_returns_successful_response()
		{
			var request = new StoreFindRequest
			{
				Criteria = new StoreFindCriteriaDto
				{
					PagingOptions = new PagingOptionsDto
					{
						PageIndex = 0,
						PageSize = 1
					}
				}
			};
			
			var response = Command.Execute(request);
			Assert.IsTrue(response.IsSuccess);
		}


		protected override ICommand<StoreFindRequest, StoreFindResponse> CreateCommand()
		{
			return new StoreFindCommand(_queryExecuterMock.Object, UnitOfWorkMock.Object);
		}
	}
}