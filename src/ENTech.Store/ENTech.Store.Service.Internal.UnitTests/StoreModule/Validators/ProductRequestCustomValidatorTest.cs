using System.Collections.Generic;
using ENTech.Store.Core;
using ENTech.Store.Core.StoreModule.Products;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.StoreModule.Requests;
using ENTech.Store.Services.Internal.StoreModule.Validators;
using ENTech.Store.Services.Internal.UnitTests.Builders;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Internal.UnitTests.StoreModule.Validators
{
	[TestFixture]
	class ProductRequestCustomValidatorTest
	{
		[SetUp]
		public void RegisterErrorCodes()
		{
			RequestValidatorErrorMessagesDictionary.Register<ProductRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<RequestValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<CommonErrorCode>();
		}

		[Test]
		public void ValidateSku_When_unique_Then_passes()
		{
			var validator = new ProductRequestCustomValidator();

			var request = ProductCreateRequestBuilder.Create().WithProduct(
							ProductDtoBuilder.Create().WithVariant(
								ProductVariantDtoBuilder.Create().WithSku("001")
								.Build())
							.Build())
						.Build();

			var errors = new ArgumentErrorsCollection();

			var dbContextMock = GetDbContextMock();

			validator.ValidateSkus(dbContextMock.Object, request, errors);

			dbContextMock.VerifyGet(c => c.Products, Times.Once);

			Assert.IsEmpty(errors);
		}

		[Test]
		public void ValidateSku_When_not_uniqe_Then_fails()
		{
			var validator = new ProductRequestCustomValidator();
			var request = ProductCreateRequestBuilder.Create()
							.WithProduct(ProductDtoBuilder.Create()
								.WithVariant(ProductVariantDtoBuilder.Create()
									.WithSku("001")
								.Build())
								.WithVariant(ProductVariantDtoBuilder.Create()
									.WithSku("000")
								.Build())
							.Build())
						.Build();

			var errors = new ArgumentErrorsCollection();

			var dbContextMock = GetDbContextMock();

			validator.ValidateSkus(dbContextMock.Object, request, errors);

			dbContextMock.VerifyGet(c => c.Products, Times.AtLeast(request.Product.Variants.Count));

			Assert.IsNotEmpty(errors);
			Assert.IsTrue(errors.HasArgument("Product.Variants.Sku"), errors[0].ArgumentName);
		}

		[Test]
		public void ValidateStoreId_When_existing_store_Then_passes()
		{
			var validator = new ProductRequestCustomValidator();
			var errors = new ArgumentErrorsCollection();

			var requestMock = new Mock<ProductGetByIdRequest>();
			var dbContextMock = GetDbContextMock();

			requestMock.SetupGet(r => r.StoreId).Returns(10);

			validator.ValidateStoreId(dbContextMock.Object, requestMock.Object, errors);

			dbContextMock.VerifyGet(c => c.Stores, Times.Once);

			Assert.IsEmpty(errors);
		}

		[Test]
		public void ValidateStoreId_When_not_existing_store_Then_fails()
		{
			var validator = new ProductRequestCustomValidator();
			var errors = new ArgumentErrorsCollection();

			var requestMock = new Mock<ProductGetByIdRequest>();
			var dbContextMock = GetDbContextMock();

			requestMock.SetupGet(r => r.StoreId).Returns(20);

			validator.ValidateStoreId(dbContextMock.Object, requestMock.Object, errors);

			dbContextMock.VerifyGet(c => c.Stores, Times.Once);

			Assert.IsNotEmpty(errors);
			Assert.IsTrue(errors.HasArgument("StoreId"));
		}

		private Mock<IDbContext> GetDbContextMock()
		{
			var products = new FakeDbSet<Product>
				{
					new Product
						{
							Variants = new List<ProductVariant>
								{
									new ProductVariant {Sku = "000"}
								}
						}
				};
			var stores = new FakeDbSet<Core.StoreModule.Store>
				{
					new Core.StoreModule.Store
						{
							Id=10
						}
				};
			var dbContextMock = new Mock<IDbContext>();
			dbContextMock.Setup(c => c.Products).Returns(products);
			dbContextMock.Setup(c => c.Stores).Returns(stores);
			
			return dbContextMock;
		}
	}
}
