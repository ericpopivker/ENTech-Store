using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.ProductModule.Validators;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.UnitTests.ProductModule.Builders;
using ENTech.Store.Services.UnitTests.StoreModule.Builders;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Validators
{
	[TestFixture]
	public class ProductValidatorTest
	{
		private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		private Mock<IProductQuery> _productQuery = new Mock<IProductQuery>();


		private ProductValidator _productValidator;
		[SetUp]
		public void SetUp()
		{
			_unitOfWorkMock.ResetCalls();
			_internalCommandService.ResetCalls();
			_productQuery.ResetCalls();

			_productValidator = new ProductValidator(_unitOfWorkMock.Object, _internalCommandService.Object, _productQuery.Object);
		}

		[Test]
		public void NameMustBeUnique_When_name_is_passed_Then_passed_to_query()
		{
			_unitOfWorkMock.Setup(uow => uow.Query(It.IsAny<ProductExiststByNameQuery>(), It.IsAny<ProductExiststByNameQuery.Criteria>())).Returns(false);

			_productValidator.NameMustBeUnique("Product.Name",  "SomeName");

			_unitOfWorkMock.Verify(uow => uow.Query(It.IsAny<ProductExiststByNameQuery>(), It.Is<ProductExiststByNameQuery.Criteria>(c => c.Name == "SomeName")), Times.Once);

		}


		[Test]
		public void NameMustBeUnique_When_name_is_unique_Then_valid()
		{
			_unitOfWorkMock.Setup(uow => uow.Query(It.IsAny<ProductExiststByNameQuery>(), It.IsAny<ProductExiststByNameQuery.Criteria>())).Returns(false);

			var argValidatorResult = _productValidator.NameMustBeUnique("Product.Name", "SomeName");

			Assert.IsTrue(argValidatorResult.IsValid);
		}
		

		[Test]
		public void NameMustBeUnique_When_name_is_not_unique_Then_invalid()
		{
			_unitOfWorkMock.Setup(uow => uow.Query(It.IsAny<ProductExiststByNameQuery>(), It.IsAny<ProductExiststByNameQuery.Criteria>())).Returns(true);

			var argValidatorResult = _productValidator.NameMustBeUnique("Product.Name", "SomeName");

			Assert.IsFalse(argValidatorResult.IsValid);
		}



		[Test]
		public void IsOverMaxProductsLimit_When_under_max_products_limit_Then_valid()
		{
			_internalCommandService.Setup(
				ics => ics.Execute<StoreGetByIdRequest, StoreGetByIdResponse, StoreGetByIdCommand>(It.IsAny<StoreGetByIdRequest>()))
				.Returns(new StoreGetByIdResponseBuilder().WithMaxProducts(10).Build());

			_productQuery.Setup(
				pqp => pqp.GetTotalByStoreId(It.IsAny<int>()))
				.Returns(5);

			var validatorResult = _productValidator.IsOverMaxProductsLimit(storeId: 123);

			Assert.IsTrue(validatorResult.IsValid);
		}


		[Test]
		public void IsOverMaxProductsLimit_When_over_max_products_limit_Then_invalid()
		{
			_internalCommandService.Setup(
				ics => ics.Execute<StoreGetByIdRequest, StoreGetByIdResponse, StoreGetByIdCommand>(It.IsAny<StoreGetByIdRequest>()))
				.Returns(new StoreGetByIdResponseBuilder().WithMaxProducts(10).Build());

			_productQuery.Setup(
				pqp => pqp.GetTotalByStoreId(It.IsAny<int>()))
				.Returns(10);

			var validatorResult = _productValidator.IsOverMaxProductsLimit(storeId: 123);

			Assert.IsFalse(validatorResult.IsValid);
		}

	}
}
