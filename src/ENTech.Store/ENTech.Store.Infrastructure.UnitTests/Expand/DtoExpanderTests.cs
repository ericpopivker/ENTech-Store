using System.Collections.Generic;
using ENTech.Store.Infrastructure.Expandable;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Expand
{
	public class DtoExpanderTests
	{
		private IDtoExpander _dtoExpander;
		private Mock<IDtoLoaderFactory> _dtoLoaderFactoryMock = new Mock<IDtoLoaderFactory>();
		private Mock<IDtoLoader<ExpandableStub>> _expandableStubLoader = new Mock<IDtoLoader<ExpandableStub>>();
		private Mock<IDtoExpandStrategyFactory> _dtoExpandStrategyFactoryMock = new Mock<IDtoExpandStrategyFactory>();
		
		public DtoExpanderTests()
		{
			_dtoExpander = new DtoExpander(_dtoLoaderFactoryMock.Object, _dtoExpandStrategyFactoryMock.Object);

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
		public void LoadAndExpand_When_called_with_parameter_for_property_Then_gets_expand_strategy_for_properties()
		{
			var entityId = 5;
			var option = new ExpandableStubExpandOption();

			var expansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();

			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(option)).Returns(expansionStrategyMock.Object);
			
			_dtoExpander.LoadAndExpand(entityId, new []{ option });

			_dtoExpandStrategyFactoryMock.Verify(x => x.Create(option), Times.Once);
		}

		[Test]
		public void LoadAndExpand_When_called_with_several_parameters_for_property_Then_gets_expand_strategies_for_properties()
		{
			var entityId = 5;
			var option1 = new ExpandableStubExpandOption();
			var option2 = new AnotherExpandableStubExpandOption();

			var expansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();
			var anotherExpansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();

			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(option1)).Returns(expansionStrategyMock.Object);
			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(option2)).Returns(anotherExpansionStrategyMock.Object);

			_dtoExpander.LoadAndExpand(entityId, new List<ExpandOption<ExpandableStub>> { option1, option2 });

			_dtoExpandStrategyFactoryMock.Verify(x => x.Create(option1), Times.Once);
			_dtoExpandStrategyFactoryMock.Verify(x => x.Create(option2), Times.Once);
		}

		[Test]
		public void LoadAndExpand_When_called_with_parameter_for_property_and_factory_returns_single_strategy_Then_executes_that_load_strategy_for_stub_entity()
		{
			var entityId = 5;
			var expandableStub = new ExpandableStub
			{
				Id = entityId,
				Name = "Hello",
				PropertyId = 5
			};
			_expandableStubLoader.Setup(x => x.Load(entityId)).Returns(expandableStub);
			var options = new ExpandableStubExpandOption();

			var expansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();

			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(options)).Returns(expansionStrategyMock.Object);

			_dtoExpander.LoadAndExpand(entityId, new []{options});

			expansionStrategyMock.Verify(x => x.Apply(expandableStub), Times.Once);
		}

		[Test]
		public void LoadAndExpand_When_called_with_parameter_for_property_and_factory_returns_multiple_strategy_Then_executes_each_load_strategy_for_stub_entity()
		{
			var entityId = 5;
			var expandableStub = new ExpandableStub
			{
				Id = entityId,
				Name = "Hello",
				PropertyId = 5
			};
			_expandableStubLoader.Setup(x => x.Load(entityId)).Returns(expandableStub);
			var option1 = new ExpandableStubExpandOption();
			var option2 = new AnotherExpandableStubExpandOption();

			var expansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();
			var secondExpansionStrategyMock = new Mock<IDtoExpandStrategy<ExpandableStub>>();


			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(option1)).Returns(expansionStrategyMock.Object);
			_dtoExpandStrategyFactoryMock.Setup(x => x.Create(option2)).Returns(secondExpansionStrategyMock.Object);

			_dtoExpander.LoadAndExpand(entityId, new List<ExpandOption<ExpandableStub>> { option1, option2 });

			expansionStrategyMock.Verify(x => x.Apply(expandableStub), Times.Once);
			secondExpansionStrategyMock.Verify(x => x.Apply(expandableStub), Times.Once);

		}

		public class ExpandableStubExpandOption : ExpandOption<ExpandableStub>
		{
			
		}

		public class AnotherExpandableStubExpandOption : ExpandOption<ExpandableStub>
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
