using ENTech.Store.Services.Expandable;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.Expand
{
	public class DtoExpanderTests
	{
		private IDtoExpander _dtoExpander;
		private Mock<IDtoLoaderFactory> _dtoLoaderFactoryMock = new Mock<IDtoLoaderFactory>();
		private Mock<IDtoLoader<ExpandableStub>> _expandableStubLoader = new Mock<IDtoLoader<ExpandableStub>>();
		
		public DtoExpanderTests()
		{
			_dtoExpander = new DtoExpander(_dtoLoaderFactoryMock.Object);

			_dtoLoaderFactoryMock.Setup(x => x.Create<ExpandableStub>()).Returns(_expandableStubLoader.Object);
		}

		[TearDown]
		public void TearDown()
		{
			_dtoLoaderFactoryMock.ResetCalls();
			_expandableStubLoader.ResetCalls();
		}

		[Test]
		public void LoadAndExpand_When_called_Then_calls_dtoLoaderFactory_create_for_typeof_expandableStub()
		{
			_dtoExpander.LoadAndExpand<ExpandableStub>(5);

			_dtoLoaderFactoryMock.Verify(x => x.Create<ExpandableStub>(), Times.Once);
		}

		[Test]
		public void LoadAndExpand_When_called_Then_calls_dtoLoader_load_with_correct_entity_id()
		{
			var entityId = 5;

			_dtoExpander.LoadAndExpand<ExpandableStub>(entityId);

			_expandableStubLoader.Verify(x => x.Load(entityId), Times.Once);
		}

		[Test]
		public void LoadAndExpand_When_called_Then_returns_entity_loaded_through_dtoLoader()
		{
			var entityId = 5;
			var expandableStub = new ExpandableStub
			{
				Id = entityId,
				Name = "Hello",
				PropertyId = 5
			};
			_expandableStubLoader.Setup(x => x.Load(entityId)).Returns(expandableStub);

			var result = _dtoExpander.LoadAndExpand<ExpandableStub>(entityId);

			Assert.AreEqual(expandableStub, result);
		}

		[Test]
		public void LoadAndExpand_When_called_with_parameter_for_property_Then_gets_loader_for_property()
		{
		}

		public class ExpandableStub : IExpandableDto
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public int PropertyId { get; set; }
			public PropertyStub Property { get; set; }
		}

		public class PropertyStub
		{
			public int Id { get; set; }
			public string Text { get; set; }
		}
	}
}
