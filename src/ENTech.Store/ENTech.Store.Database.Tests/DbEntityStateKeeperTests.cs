using ENTech.Store.Database.Exceptions;
using ENTech.Store.Database.Utility;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Entities;
using NUnit.Framework;

namespace ENTech.Store.Database.Tests
{
	public class DbEntityStateKeeperTests
	{
		private readonly IDbEntityStateKeeper<StubDomainEntity, StubDbEntity> _dbEntityStateKeeper = new DbEntityStateKeeper<StubDomainEntity, StubDbEntity>();

		[TearDown]
		public void TearDown()
		{
			_dbEntityStateKeeper.Clear();
		}

		[Test]
		public void Store_When_passes_entities_Then_does_not_throw()
		{
			var stubEntity = new StubDomainEntity();
			var stubDbEntity = new StubDbEntity();

			Assert.DoesNotThrow(()=> _dbEntityStateKeeper.Store(stubEntity, stubDbEntity));
		}

		[Test]
		public void Store_When_passes_same_entities_again_Then_throws()
		{
			var stubEntity = new StubDomainEntity();
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);

			Assert.Throws<EntityTrackedException>(()=> _dbEntityStateKeeper.Store(stubEntity, stubDbEntity));
		}

		[Test]
		public void Store_When_passes_entity_then_removes_it_then_passes_again_Then_does_not_throw()
		{
			var stubEntity = new StubDomainEntity();
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);
			_dbEntityStateKeeper.Remove(stubEntity);

			Assert.DoesNotThrow(() => _dbEntityStateKeeper.Store(stubEntity, stubDbEntity));
		}

		[Test]
		public void Get_When_entity_was_passed_before_Then_returns_this_entity()
		{
			var stubEntity = new StubDomainEntity {Id = 0};
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);

			var dbEntity = _dbEntityStateKeeper.Get(stubEntity);

			Assert.AreEqual(stubDbEntity, dbEntity);
		}

		[Test]
		public void Get_When_entity_was_not_passed_before_Then_throws()
		{
			var stubEntity = new StubDomainEntity {Id = 0};

			Assert.Throws<EntityNotTrackedException>(() => _dbEntityStateKeeper.Get(stubEntity));
		}

		[Test]
		public void
			Get_When_entity_was_passed_before_and_another_entity_with_same_field_values_is_passed_to_method_Then_throws()
		{
			var stubEntity = new StubDomainEntity{Id = 1};
			var stubDbEntity = new StubDbEntity();

			var anotherEntity = new StubDomainEntity{ Id = 1};

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);

			Assert.Throws<EntityNotTrackedException>(() => _dbEntityStateKeeper.Get(anotherEntity));
		}

		[Test]
		public void Get_When_entity_was_stored_before_then_removed_Then_throws()
		{
			var stubEntity = new StubDomainEntity { Id = 0 };
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);

			_dbEntityStateKeeper.Remove(stubEntity);

			Assert.Throws<EntityNotTrackedException>(() => _dbEntityStateKeeper.Get(stubEntity));
		}

		[Test]
		public void Remove_When_entity_was_not_passed_before_Then_throws()
		{
			var stubEntity = new StubDomainEntity {Id = 0};

			Assert.Throws<EntityNotTrackedException>(() => _dbEntityStateKeeper.Remove(stubEntity));
		}
		
		[Test]
		public void Remove_When_entity_was_stored_before_Then_does_no_throw()
		{
			var stubEntity = new StubDomainEntity {Id = 0};
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);

			Assert.DoesNotThrow(() => _dbEntityStateKeeper.Remove(stubEntity));
		}

		[Test]
		public void Get_When_entity_was_stored_before_then_clear_happened_Then_cannot_get_entity()
		{
			var stubEntity = new StubDomainEntity { Id = 0 };
			var stubDbEntity = new StubDbEntity();

			_dbEntityStateKeeper.Store(stubEntity, stubDbEntity);
			_dbEntityStateKeeper.Clear();

			Assert.Throws<EntityNotTrackedException>(() => _dbEntityStateKeeper.Get(stubEntity));
		}

		public class StubDomainEntity : IDomainEntity
		{
			public int Id { get; set; }
		}

		public class StubDbEntity : IDbEntity
		{
			public int Id { get; set; }
		}
	}
}