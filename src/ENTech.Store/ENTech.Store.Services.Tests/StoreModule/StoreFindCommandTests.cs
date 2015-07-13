using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Dtos;
using ENTech.Store.Services.StoreModule;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Projections;
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
		private readonly Mock<IFilterableDbSet<Entities.StoreModule.Store>> _storesMock = new Mock<IFilterableDbSet<Entities.StoreModule.Store>>();
		private readonly IEnumerable<StoreProjection> _findResult = new List<StoreProjection>
		{
			new StoreProjection{Id = 1},
			new StoreProjection{Id = 2}
		};

		public StoreFindCommandTests()
		{
			Mock<IDbContext> dbContextMock = new Mock<IDbContext>();

			var dbSetData = new ObservableCollection<Entities.StoreModule.Store>();
			var dataQueryable = dbSetData.AsQueryable();

			_storesMock.Setup(x => x.Local).Returns(dbSetData);

			_storesMock.Setup(x => x.Expression).Returns(dataQueryable.Expression);
			_storesMock.Setup(x => x.ElementType).Returns(dataQueryable.ElementType);
			_storesMock.Setup(x => x.Provider).Returns(dataQueryable.Provider);

			dbContextMock.SetupGet(x => x.Stores).Returns(_storesMock.Object);

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
		public void Execute_When_called_with_valid_criteria_Then_uses_mapper_to_map_results()
		{
			var request = GetStoreFindRequest();

			Command.Execute(request);

			MapperMock.Verify(x=>x.Map<IEnumerable<StoreProjection>, IEnumerable<StoreDto>>(_findResult), Times.Once);
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
			return new StoreFindCommand(_queryExecuterMock.Object, UnitOfWorkMock.Object, MapperMock.Object);
		}
	}
}