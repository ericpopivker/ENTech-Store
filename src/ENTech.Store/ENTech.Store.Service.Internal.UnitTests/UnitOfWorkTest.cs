using System;
using Moq;
using NUnit.Framework;
using ENTech.Store.Entities;
using IDbContext = ENTech.Store.Entities.IDbContext;


namespace ENTech.Store.Services.Internal.UnitTests
{
	[TestFixture]
	internal class UnitOfWorkTest
	{

		private Mock<IDbContextFactory>_dbContextFactoryMock;
		private Mock<IDbContext> _dbContextMock;


		[SetUp]
		public void Setup()
		{
			
			_dbContextFactoryMock = new Mock<IDbContextFactory>();
			_dbContextMock = new Mock<IDbContext>();

			_dbContextFactoryMock.Setup(x => x.Create()).Returns(_dbContextMock.Object);
		}


		[TearDown]
		public void TearDown()
		{
			//Have to dispose to make sure that it is done after every test
			//If it is not disposed DbContext maybe available between tests which is bad idea
			var uow = new UnitOfWork(_dbContextFactoryMock.Object);
			uow.Dispose();
		}


		[Test]
		public void Dispose_Then_calls_DbContext_Dispose()
		{
			var uow = new UnitOfWork(_dbContextFactoryMock.Object);

			uow.Dispose();

			_dbContextMock.Verify(x => x.Dispose(), Times.Once);
		}

		[Test]
		public void When_multiple_instances_And_not_disposed_Then_single_DbContext_instance()
		{
			var uow1 = new UnitOfWork(_dbContextFactoryMock.Object);

			var dbContextMock2 = new Mock<IDbContext>();
			_dbContextFactoryMock.Setup(x => x.Create()).Returns(dbContextMock2.Object);

			var uow2 = new UnitOfWork(_dbContextFactoryMock.Object);

			Assert.AreEqual(uow1.DbContext, uow2.DbContext);
		}

		[Test]
		public void When_multiple_instances_And_disposed_Then_multiple_DbContext_instances()
		{
			var uow1 = new UnitOfWork(_dbContextFactoryMock.Object);
			var dbContext1 = uow1.DbContext;
			uow1.Dispose();

			var dbContextMock2 = new Mock<IDbContext>();
			_dbContextFactoryMock.Setup(x => x.Create()).Returns(dbContextMock2.Object);

			var uow2 =  new UnitOfWork(_dbContextFactoryMock.Object);
			Assert.AreNotEqual(dbContext1, uow2.DbContext);
		}

		[Test]
		public void Dispose_Then_throw_exception_on_DbContext_get()
		{
			var uow1 = new UnitOfWork(_dbContextFactoryMock.Object);
			uow1.Dispose();

			Assert.Throws<ObjectDisposedException>(() => { var dbContext = uow1.DbContext; });
		}

		[Test]
		public void BeginTransaction_calls_DbContext_BeginTransaction()
		{
			var uow = new UnitOfWork(_dbContextFactoryMock.Object);

			uow.BeginTransaction();

			_dbContextMock.Verify(x => x.BeginTransaction(), Times.Once);
		}

		[Test]
		public void CompleteTransaction_calls_DbContext_CompleteTransaction()
		{
			var uow = new UnitOfWork(_dbContextFactoryMock.Object);

			uow.CompleteTransaction();

			_dbContextMock.Verify(x => x.CompleteTransaction(), Times.Once);
		}


		[Test]
		public void RollbackTransaction_calls_DbContext_RollbackTransaction()
		{
			var uow = new UnitOfWork(_dbContextFactoryMock.Object);

			uow.RollbackTransaction();

			_dbContextMock.Verify(x => x.RollbackTransaction(), Times.Once);
		}

	}
}
