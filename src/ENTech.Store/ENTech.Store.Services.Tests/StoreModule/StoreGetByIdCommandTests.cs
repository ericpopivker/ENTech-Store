using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Projections;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreGetByIdCommandTests : CommandTestsBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly Mock<IQueryExecuter> _queryExecuterMock = new Mock<IQueryExecuter>();

		protected override void TearDownInternal()
		{
			_queryExecuterMock.ResetCalls();
		}

		public StoreGetByIdCommandTests()
		{
			_queryExecuterMock.Setup(x => x.Execute(It.IsAny<QueryCriteria<StoreProjection>>()))
				.Returns((GetByIdCriteria<StoreProjection> criteria) => new StoreProjection
				{
					Id = criteria.Id
				});
		}

		[Test]
		public void Execute_When_called_Then_calls_for_query_provider()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = 1
			});

			_queryExecuterMock.Verify(mock => mock.Execute(It.IsAny<QueryCriteria<StoreProjection>>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_Then_passes_correct_criteria_type_to_query_executer()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = 1
			});

			_queryExecuterMock.Verify(mock => mock.Execute(It.IsAny<GetByIdCriteria<StoreProjection>>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_concrete_id_Then_passes_that_id_into_criteria()
		{
			var id = 152125;

			Command.Execute(new StoreGetByIdRequest
			{
				Id = id
			});

			_queryExecuterMock.Verify(mock => mock.Execute(It.Is<GetByIdCriteria<StoreProjection>>(proj => proj.Id == id)), Times.Once);
		}

		[Test]
		public void Execute_When_called_Then_uses_mapper_to_map_project()
		{
			var id = 152125;

			Command.Execute(new StoreGetByIdRequest
			{
				Id = id
			});

			MapperMock.Verify(mock => mock.Map<StoreProjection, StoreDto>(It.Is<StoreProjection>(proj => proj.Id == id)), Times.Once);
		}

		protected override ICommand<StoreGetByIdRequest, StoreGetByIdResponse> CreateCommand()
		{
			return new StoreGetByIdCommand(UnitOfWorkMock.Object, _queryExecuterMock.Object, MapperMock.Object);
		}
	}
}