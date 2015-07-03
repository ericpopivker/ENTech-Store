using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using Moq;
using NUnit.Framework;
using ENTech.Store.Services.ProductModule.Commands;

namespace ENTech.Store.Services.UnitTests.ProductModule.Commands
{
	[TestFixture]
	public class ProductCreateCommandTest
	{
		private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		private Mock<IDtoValidatorFactory> _dtoValidatorFactorykMock = new Mock<IDtoValidatorFactory>();
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		private Mock<IProductQuery> _productQuery = new Mock<IProductQuery>();
		private Mock<IProductValidator> _productValidator = new Mock<IProductValidator>();


		private ProductCreateCommand _createCommand;
		[SetUp]
		public void SetUp()
		{
			_unitOfWorkMock.ResetCalls();
			_dtoValidatorFactorykMock.ResetCalls();
			_internalCommandService.ResetCalls();
			_productQuery.ResetCalls();

			_createCommand = new ProductCreateCommand(_unitOfWorkMock.Object, _dtoValidatorFactorykMock.Object, _internalCommandService.Object, _productQuery.Object, _productValidator.Object);
		}

	}
	
}
