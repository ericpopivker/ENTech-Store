using System;
using System.Collections.ObjectModel;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Database.Repository.Exceptions;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class RepositoryTests
	{
		private readonly IRepository<StubEntity> _repository;
		private readonly IRepository<StubEntityWithNoDbSet> _repositoryForNotRegisteredInDbSetResolverEntity;

		private readonly Mock<IDbSetResolver> _dataSetResolver;

		private readonly StubEntity _firstEntity = new StubEntity { Id = 1 };
		private readonly StubEntity _secondEntity = new StubEntity { Id = 2 };
		private readonly StubEntity _thirdEntity = new StubEntity { Id = 3 };
		private readonly StubEntity _fourthEntityWithDupeId = new StubEntity { Id = 4 };
		private readonly StubEntity _fifthEntityWithDupeId = new StubEntity { Id = 4 };

		public RepositoryTests()
		{
			_dataSetResolver = new Mock<IDbSetResolver>();
			_repository = new Repository<StubEntity>(_dataSetResolver.Object);
			_repositoryForNotRegisteredInDbSetResolverEntity = new Repository<StubEntityWithNoDbSet>(_dataSetResolver.Object);

			var fakeDbSet = new FakeDbSet<StubEntity>(new ObservableCollection<StubEntity>
			{
				_firstEntity,
				_secondEntity,
				_thirdEntity,
				_fourthEntityWithDupeId,
				_fifthEntityWithDupeId
			});

			_dataSetResolver.Setup(x => x.Resolve<StubEntity>()).Returns(fakeDbSet);
		}

		[Test]
		public void GetById_When_called_for_entity_that_is_registered_ib_dbSetResolver_Then_calls_for_dataSetResolver_resolve_to_get_dataset_for_stubEntity()
		{
			var entityId = _firstEntity.Id;

			_repository.GetById(entityId);

			_dataSetResolver.Verify(x=>x.Resolve<StubEntity>());
		}

		[Test]
		public void GetById_When_called_for_entity_that_is_not_registered_ib_dbSetResolver_Then_calls_for_dataSetResolver_resolve_to_get_dataset_for_stubEntity()
		{
			var entityId = 1;

			Assert.Throws<NonPersistentEntityException<StubEntityWithNoDbSet>>(() => _repositoryForNotRegisteredInDbSetResolverEntity.GetById(entityId));
		}

		[Test]
		public void GetById_When_called_Then_gets_item_with_correct_id_from_collecion()
		{
			var entityId = _firstEntity.Id;

			var result = _repository.GetById(entityId);

			Assert.AreEqual(_firstEntity, result);
		}

		[Test]
		public void GetById_When_called_for_entity_with_dupe_id_Then_does_not_throw()
		{
			var entityId = _fourthEntityWithDupeId.Id;

			Assert.DoesNotThrow(()=>_repository.GetById(entityId));
		}

		[Test]
		public void Add_When_called_Then_calls_for_dataSetResolver_resolve_to_get_dataset_for_stubEntity()
		{
			var newEntity = new StubEntity();

			_repository.Add(newEntity);

			_dataSetResolver.Verify(x => x.Resolve<StubEntity>());
		}

		public class StubEntity : IEntity
		{
			public int Id { get; set; }
		}

		public class StubEntityWithNoDbSet : IEntity
		{
			public int Id { get; set; }
		}
	}
}