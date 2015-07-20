using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.Tests.Shared;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreGetByIdCommandTests : CommandTestsBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();

		protected override void TearDownInternal()
		{
			_storeRepositoryMock.ResetCalls();
		}

		public StoreGetByIdCommandTests()
		{
			_storeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns((int id) => new Entities.StoreModule.Store
				{
					Id = id
				});
		}

		[Test]
		public void Execute_When_called_Then_calls_for_query_provider()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = 1
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_Then_passes_correct_criteria_type_to_query_executer()
		{
			Command.Execute(new StoreGetByIdRequest
			{
				Id = 1
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void Execute_When_called_with_concrete_id_Then_passes_that_id_into_criteria()
		{
			var storeId = 152125;

			Command.Execute(new StoreGetByIdRequest
			{
				Id = storeId
			});

			_storeRepositoryMock.Verify(mock => mock.GetById(It.Is<int>(id => id == storeId)), Times.Once);
		}

		[Test]
		public void Execute_When_called_Then_uses_mapper_to_map_project()
		{
			var id = 152125;

			Command.Execute(new StoreGetByIdRequest
			{
				Id = id
			});

			MapperMock.Verify(mock => mock.Map<Entities.StoreModule.Store, StoreDto>(It.Is<Entities.StoreModule.Store>(proj => proj.Id == id)), Times.Once);
		}

		protected override ICommand<StoreGetByIdRequest, StoreGetByIdResponse> CreateCommand()
		{
			return new StoreGetByIdCommand(_storeRepositoryMock.Object, UnitOfWorkMock.Object, MapperMock.Object);
		}
	}
}