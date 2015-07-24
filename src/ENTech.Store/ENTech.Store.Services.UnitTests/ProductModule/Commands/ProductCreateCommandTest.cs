using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Commands
{
	[TestFixture]
	public class ProductCreateCommandTest
	{
		private Mock<IDtoValidatorFactory> _dtoValidatorFactorykMock = new Mock<IDtoValidatorFactory>();
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		private Mock<IProductQuery> _productQuery = new Mock<IProductQuery>();
		private Mock<IProductValidator> _productValidator = new Mock<IProductValidator>();
		private Mock<IStoreValidator> _storeValidator = new Mock<IStoreValidator>();


		private ProductCreateCommand _createCommand;

		[SetUp]
		public void SetUp()
		{
			_dtoValidatorFactorykMock.ResetCalls();
			_internalCommandService.ResetCalls();
			_productQuery.ResetCalls();

			_productValidator.ResetCalls();
			_storeValidator.ResetCalls();

			_createCommand = new ProductCreateCommand(_dtoValidatorFactorykMock.Object, _internalCommandService.Object, _productQuery.Object, _productValidator.Object, _storeValidator.Object);
		}
	}
}
