using System;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Services.Repositories
{
	[TestFixture]
	public class CachedRepositoryTest
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

		private CachedRepository<EntityStub> _cachedRepository;

		private Mock<IRepository<EntityStub>> _dbRepository = new Mock<IRepository<EntityStub>>();
		private Mock<ICache> _l2Cache = new Mock<ICache>();
		private Mock<ICache> _l3Cache = new Mock<ICache>();


		private const int EntityIdFake = 123;

		[SetUp]
		public void SetUp()
		{
			_dbRepository.ResetCalls();
			_l2Cache.ResetCalls();
			_l3Cache.ResetCalls();

			_cachedRepository = new CachedRepository<EntityStub>(_dbRepository.Object, _l2Cache.Object, _l3Cache.Object);

		}

		[Test]
		public void GetById_When_not_in_l3Cache_And_not_in_l2CAche_Then_gets_from_Db()
		{
			string outValue = null;

			_l3Cache.Setup(cache => cache.TryGet(It.IsAny<string>(), ref outValue)).Returns(false);
			_l2Cache.Setup(cache => cache.TryGet(It.IsAny<string>(), ref outValue)).Returns(false);
			_dbRepository.Setup(dbRepo => dbRepo.GetById(It.IsAny<int>())).Returns(new EntityStub());

			_cachedRepository.GetById(EntityIdFake);

			_dbRepository.Verify(dbRepo => dbRepo.GetById(It.Is<int>(entityId => entityId == EntityIdFake)), Times.Once);

		}
	}
}
