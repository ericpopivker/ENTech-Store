using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Validators;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.UnitTests.ProductModule.Builders;
using ENTech.Store.Services.UnitTests.StoreModule.Builders;
using Moq;
using NUnit.Framework;
using ENTech.Store.Services.ProductModule.Commands;

namespace ENTech.Store.Services.UnitTests.ProductModule.Commands
{
	[TestFixture]
	public class ProductCreateCommandTest
	{
		private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		private Mock<IProductQuery> _productQuery = new Mock<IProductQuery>();
		private Mock<IProductValidator> _productValidator = new Mock<IProductValidator>();


		private ProductCreateCommand _createCommand;
		[SetUp]
		public void SetUp()
		{
			 _unitOfWorkMock.ResetCalls();
			_internalCommandService.ResetCalls();
			_productQuery.ResetCalls();

			_createCommand = new ProductCreateCommand(_unitOfWorkMock.Object, _internalCommandService.Object, _productQuery.Object, _productValidator.Object);
		}

	}
	
}
