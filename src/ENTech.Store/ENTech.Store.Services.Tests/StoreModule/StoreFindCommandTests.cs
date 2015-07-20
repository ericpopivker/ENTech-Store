using System.Collections.Generic;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Database.Repository;
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
		private readonly Mock<IStoreQuery> _queryExecuterMock = new Mock<IStoreQuery>();
		private readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();
		private readonly IEnumerable<int> _findResult = new[] {1, 2};

		private readonly IEnumerable<Entities.StoreModule.Store> _domainEntities = new List<Entities.StoreModule.Store>
		{
			new Entities.StoreModule.Store {Id = 1},
			new Entities.StoreModule.Store {Id = 2}
		};

		public StoreFindCommandTests()
		{
			Mock<IDbContext> dbContextMock = new Mock<IDbContext>();
			
			_storeRepositoryMock.Setup(x => x.FindByIds(_findResult)).Returns(_domainEntities);

			UnitOfWorkMock.Setup(x => x.DbContext).Returns(dbContextMock.Object);

			_queryExecuterMock.Setup(x => x.Find(It.IsAny<StoreFindCriteria>())).Returns(_findResult);
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

		[Test]
		public void Execute_When_called_with_valid_criteria_Then_uses_mapper_to_map_criteria()
		{
			var request = GetStoreFindRequest();

			Command.Execute(request);

			MapperMock.Verify(x => x.Map<StoreFindCriteriaDto, StoreFindCriteria>(request.Criteria), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_valid_criteria_Then_uses_repository_findByIds_with_query_result()
		{
			var request = GetStoreFindRequest();

			Command.Execute(request);

			_storeRepositoryMock.Verify(x=>x.FindByIds(It.Is<IEnumerable<int>>(y=> y.Equals(_findResult))));
		}

		[Test]
		public void Execute_When_called_with_valid_criteria_Then_uses_mapper_to_map_results()
		{
			var request = GetStoreFindRequest();

			Command.Execute(request);

			MapperMock.Verify(x => x.Map<IEnumerable<Entities.StoreModule.Store>, IEnumerable<StoreDto>>(_domainEntities), Times.Once);
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
			return new StoreFindCommand(_storeRepositoryMock.Object, _queryExecuterMock.Object, MapperMock.Object);
		}
	}
}