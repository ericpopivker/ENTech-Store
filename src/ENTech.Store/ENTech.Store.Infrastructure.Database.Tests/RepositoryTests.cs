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

		private readonly StubEntity _firstEntity = new StubEntity { Id = 1 };
		private readonly StubEntity _secondEntity = new StubEntity { Id = 2 };
		private readonly StubEntity _thirdEntity = new StubEntity { Id = 3 };
		private readonly StubEntity _fourthEntityWithDupeId = new StubEntity { Id = 4 };
		private readonly StubEntity _fifthEntityWithDupeId = new StubEntity { Id = 4 };

		public RepositoryTests()
		{
			var fakeDbSet = new FakeDbSet<StubEntity>(new ObservableCollection<StubEntity>
			{
				_firstEntity,
				_secondEntity,
				_thirdEntity,
				_fourthEntityWithDupeId,
				_fifthEntityWithDupeId
			});

			_repository = new Repository<StubEntity>(fakeDbSet);
		}

		[Test]
		public void Ctor_When_called_with_null_dbset_Then_throws_argumentNullException()
		{
			Assert.Throws<ArgumentNullException>(()=> new Repository<StubEntityWithNoDbSet>(null));
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