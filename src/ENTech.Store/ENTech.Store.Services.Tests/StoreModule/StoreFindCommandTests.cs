using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Dtos;
using ENTech.Store.Services.StoreModule;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
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

		public StoreFindCommandTests()
		{
			Mock<IDbContext> dbContextMock = new Mock<IDbContext>();
			dbContextMock.SetupGet(x => x.Stores).Returns(new FakeDbContext.FakeDbSet<Entities.StoreModule.Store>());

			UnitOfWorkMock.Setup(x => x.DbContext).Returns(dbContextMock.Object);
		}

		[Test]
		public void Execute_When_called_with_valid_criteria_Then_returns_successful_response()
		{
			var request = GetStoreFindRequest();
			
			var response = Command.Execute(request);

			Assert.IsTrue(response.IsSuccess);
		}

		[Test]
		public void Execute_When_called_with_valid_criteria_Then_uses_storeQueryExecuter_to_get_data()
		{
			var request = GetStoreFindRequest();

			Command.Execute(request);

			_queryExecuterMock.Verify(x=>x.Find(It.IsAny<StoreFindCriteria>()));
		}


		private static StoreFindRequest GetStoreFindRequest()
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
			return request;
		}

		protected override ICommand<StoreFindRequest, StoreFindResponse> CreateCommand()
		{
			return new StoreFindCommand(_queryExecuterMock.Object, UnitOfWorkMock.Object);
		}
	}
}