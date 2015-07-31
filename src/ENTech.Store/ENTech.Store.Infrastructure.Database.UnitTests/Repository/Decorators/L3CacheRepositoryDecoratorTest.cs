using System;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Database.Repository.Decorators;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.UnitTests.Repository.Decorators
{
	[TestFixture]
	public class L3CacheRepositoryDecoratorTest
	{
		public class EntityStub : IDomainEntity, IAuditable
		{
			public int Id { get; set; }

			public DateTime CreatedAt { get; set; }

			public DateTime LastUpdatedAt { get; set; }

			public EntityStub()
			{
				Id = EntityIdFake;
				CreatedAt = DateTime.UtcNow;
				LastUpdatedAt = DateTime.UtcNow;
			}
		}

		private L3CacheRepositoryDecorator<EntityStub> _l3CacheDecorator;

		private Mock<IRepository<EntityStub>> _dbRepository = new Mock<IRepository<EntityStub>>();
		private Mock<IDistributedCache> _l3Cache = new Mock<IDistributedCache>();


		private const int EntityIdFake = 123;

		[SetUp]
		public void SetUp()
		{
			_dbRepository.ResetCalls();
			_l3Cache.ResetCalls();

			_l3CacheDecorator = new L3CacheRepositoryDecorator<EntityStub>(_dbRepository.Object, _l3Cache.Object);

		}

		[Test]
		public void GetById_When_not_in_l3Cache_Then_gets_from_Db()
		{
			string outValue = null;

			_l3Cache.Setup(cache => cache.TryGet(It.IsAny<string>(), ref outValue)).Returns(false);
			_dbRepository.Setup(dbRepo => dbRepo.GetById(It.IsAny<int>())).Returns(new EntityStub());

			_l3CacheDecorator.GetById(EntityIdFake);

			_dbRepository.Verify(dbRepo => dbRepo.GetById(It.Is<int>(entityId => entityId == EntityIdFake)), Times.Once);

		}


		[Test]
		public void GetById_When_not_in_l3Cache_Then_adds_to_l3Cache()
		{
			string outValue = null;

			_l3Cache.Setup(cache => cache.TryGet(It.IsAny<string>(), ref outValue)).Returns(false);

			var entityStub = new EntityStub();
			_dbRepository.Setup(dbRepo => dbRepo.GetById(It.IsAny<int>())).Returns(entityStub);

			_l3CacheDecorator.GetById(EntityIdFake);

			_l3Cache.Verify(
				cache =>
					cache.Set(It.IsAny<string>(), It.Is<EntityStub>(entity => entity == entityStub),
						It.Is<CacheOpts>(val => val == null)), Times.Once);

		}


		[Test]
		public void Delete_When_called_Then_tries_to_delete_from_l3Cache()
		{
			var entityStub = new EntityStub {Id = EntityIdFake};
			_l3CacheDecorator.Delete(entityStub);

			_l3Cache.Verify(
				cache =>
					cache.TryRemove(It.Is<string>(key => key.Contains(EntityIdFake.ToString()))), Times.AtLeastOnce);

		}
	}
}
