using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class QueryBaseTests
	{
		private IQuery _stubEntityQuery;
		private readonly ObservableCollection<StubEntityDbEntity> _dbSetData;

		private Mock<IDbSet<StubEntityDbEntity>> _dbSetMock = new Mock<IDbSet<StubEntityDbEntity>>();

		public QueryBaseTests()
		{
			_dbSetData = new ObservableCollection<StubEntityDbEntity>();

			var dataQueryable = _dbSetData.AsQueryable();

			_dbSetMock.Setup(x => x.Local).Returns(_dbSetData);

			_dbSetMock.Setup(x => x.Expression).Returns(dataQueryable.Expression);
			_dbSetMock.Setup(x => x.ElementType).Returns(dataQueryable.ElementType);
			_dbSetMock.Setup(x => x.Provider).Returns(dataQueryable.Provider);

			_stubEntityQuery = new QueryBase<StubEntityDbEntity>(_dbSetMock.Object);
		}

		[Test]
		public void GetById_When_called_Then_returns_entity_from_projectionDbContext()
		{
		}

		public class StubEntityDbEntity : IDbEntity
		{
			public int Id { get; set; }
		}
	}
}