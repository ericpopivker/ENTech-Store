using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class RepositoryTests
	{
		private readonly IRepository<StubEntity> _repository;

		private Mock<IDbSet<StubEntity>> _dbSetMock = new Mock<IDbSet<StubEntity>>();
		private Mock<IDbEntityStateManager<StubEntity>> _entityStateManager = new Mock<IDbEntityStateManager<StubEntity>>();

		private readonly StubEntity _firstEntity = new StubEntity { Id = 1 };
		private readonly StubEntity _secondEntity = new StubEntity { Id = 2 };
		private readonly StubEntity _thirdEntity = new StubEntity { Id = 3 };
		private readonly StubEntity _fourthEntityWithDupeId = new StubEntity { Id = 4 };
		private readonly StubEntity _fifthEntityWithDupeId = new StubEntity { Id = 4 };

		private readonly ObservableCollection<StubEntity> _dbSetData;

		public RepositoryTests()
		{
			_dbSetData = new ObservableCollection<StubEntity>
			{
				_firstEntity,
				_secondEntity,
				_thirdEntity,
				_fourthEntityWithDupeId,
				_fifthEntityWithDupeId
			};

			var dataQueryable = _dbSetData.AsQueryable();

			_dbSetMock.Setup(x => x.Local).Returns(_dbSetData);

			_dbSetMock.Setup(x => x.Expression).Returns(dataQueryable.Expression);
			_dbSetMock.Setup(x => x.ElementType).Returns(dataQueryable.ElementType);
			_dbSetMock.Setup(x => x.Provider).Returns(dataQueryable.Provider);

			_repository = new Repository<StubEntity>(_dbSetMock.Object, _entityStateManager.Object);
		}

		[Test]
		public void Ctor_When_called_with_null_dbset_and_entityStateManager_Then_throws_argumentNullException()
		{
			Assert.Throws<ArgumentNullException>(()=> new Repository<StubEntity>(null, null));
		}

		[Test]
		public void GetById_When_called_Then_gets_item_with_correct_id_from_collecion()
		{
			var entityId = _thirdEntity.Id;

			var result = _repository.GetById(entityId);

			Assert.AreEqual(_thirdEntity, result);
		}

		[Test]
		public void GetById_When_called_for_entity_with_dupe_id_Then_does_not_throw()
		{
			var entityId = _fourthEntityWithDupeId.Id;

			Assert.DoesNotThrow(()=>_repository.GetById(entityId));
		}

		[Test]
		public void Add_When_called_for_entity_with_dupe_id_Then_calls_dbSet_add()
		{
			var stubEntity = new StubEntity();

			_repository.Add(stubEntity);

			_dbSetMock.Verify(x => x.Add(stubEntity));
		}

		[Test]
		public void Delete_When_called_for_entity_without_logically_deletable_interface_Then_calls_dbSet_remove()
		{
			var stubEntity = new StubEntity();

			_repository.Delete(stubEntity);

			_dbSetMock.Verify(x => x.Remove(stubEntity));
		}

		[Test]
		public void Update_When_called_for_entity_without_auditable_interface_Then_calls_entity_state_manager_changeState()
		{
			var stubEntity = new StubEntity();

			_repository.Update(stubEntity);

			_entityStateManager.Verify(x => x.MarkUpdated(stubEntity));
		}

		public class StubEntity : IEntity
		{
			public int Id { get; set; }
		}
	}
}