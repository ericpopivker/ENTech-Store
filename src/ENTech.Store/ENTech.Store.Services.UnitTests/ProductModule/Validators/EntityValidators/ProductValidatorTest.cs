using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using ENTech.Store.Services.UnitTests.StoreModule.Builders;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Validators.EntityValidators
{
	[TestFixture]
	public class ProductValidatorTest
	{
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		private Mock<IProductQuery> _productQuery = new Mock<IProductQuery>();


		private ProductValidator _productValidator;

		public const string ProductNameFake = "SomeName";
		public const int StoreIdFake = 23;

		[SetUp]
		public void SetUp()
		{
			_internalCommandService.ResetCalls();
			_productQuery.ResetCalls();

			_productValidator = new ProductValidator(_internalCommandService.Object, _productQuery.Object);
		}

		[Test]
		public void NameMustBeUnique_When_name_is_passed_Then_passed_to_query()
		{
			_productQuery.Setup(pq => pq.ExistsByName(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

			_productValidator.NameMustBeUnique(ProductNameFake,  StoreIdFake);

			_productQuery.Verify(pq => pq.ExistsByName(It.Is<string>(name => name == ProductNameFake), It.Is<int>(storeId => storeId  == StoreIdFake)), Times.Once);

		}


		[Test]
		public void NameMustBeUnique_When_name_is_unique_Then_valid()
		{
			_productQuery.Setup(pq => pq.ExistsByName(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

			var argValidatorResult = _productValidator.NameMustBeUnique(ProductNameFake,  StoreIdFake);

			Assert.IsTrue(argValidatorResult.IsValid);
		}
		

		[Test]
		public void NameMustBeUnique_When_name_is_not_unique_Then_invalid()
		{
			_productQuery.Setup(pq => pq.ExistsByName(It.IsAny<string>(), It.IsAny<int>())).Returns(true);

			var argValidatorResult = _productValidator.NameMustBeUnique(ProductNameFake,  StoreIdFake);

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
