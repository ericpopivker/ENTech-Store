using System.Collections.ObjectModel;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class RepositoryTests
	{
		private readonly IRepository<StubEntity> _repository;
		private readonly Mock<IDbSetResolver> _dataSetResolver;

		public RepositoryTests()
		{
			_dataSetResolver = new Mock<IDbSetResolver>();
			_repository = new Repository<StubEntity>(_dataSetResolver.Object);


			var fakeDbSet = new FakeDbSet<StubEntity>(new ObservableCollection<StubEntity>{new StubEntity{Id = 1}});

			_dataSetResolver.Setup(x => x.Resolve<StubEntity>()).Returns(fakeDbSet);
		}

		[Test]
		public void GetById_When_called_Then_calls_for_dataSetResolver_resolve_to_get_dataset_for_stubEntity()
		{
			_repository.GetById(1);

			_dataSetResolver.Verify(x=>x.Resolve<StubEntity>());
		}
	}

	public class StubEntity : IEntity
	{
		public int Id { get; set; }
	}
}