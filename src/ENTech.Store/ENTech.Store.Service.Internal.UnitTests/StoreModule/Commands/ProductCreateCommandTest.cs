using System.Linq;
using ENTech.Store.Core.StoreModule.Products;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.StoreModule.Commands;
using ENTech.Store.Services.Internal.StoreModule.Requests;
using ENTech.Store.Services.Internal.StoreModule.Responses;
using ENTech.Store.Services.Internal.StoreModule.Validators;
using ENTech.Store.Services.Internal.UnitTests.Builders;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using IDbContext = ENTech.Store.Core.IDbContext;

namespace ENTech.Store.Services.Internal.UnitTests.StoreModule.Commands
{
	[TestFixture]
	internal class ProductCreateCommandTest :
		CommandTestBase
			<IDbContext, ProductCreateCommand, ProductCreateRequest, ProductCreateOrUpdateResponse, ProductRequestCustomValidator,
			ProductRequestCustomValidatorErrorCode>
	{
		[SetUp]
		public void RegisterErrorCodes()
		{
			RequestValidatorErrorMessagesDictionary.Register<ProductRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<RequestValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<CommonErrorCode>();
		}

		[Test]
		public void ValidateRequestCustom_When_correct_productVaraintSkus_Then_success_validation()
		{
			ProductCreateRequest request = ProductCreateRequestBuilder.Create()
											.WithProduct(ProductDtoBuilder.Create()
												.WithName("TEST")
												.WithCategoryId(1)
												.WithDescription("TEST DESCRIPTION")
												.WithOption(ProductOptionDtoBuilder.Create()
													.WithIndex(1)
													.WithName("TEST")
													.Build())
												.WithVariant(ProductVariantDtoBuilder.Create()
													.WithPrice(10)
													.WithQuantity(10)
													.WithOptionValue(ProductVariantOptionValueDtoBuilder.Create()
														.WithIndex(1)
														.WithValue("TEST")
														.Build())
													.Build())
												.Build())
											.Build();
			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(
				s => s.ValidateSkus(It.IsAny<IDbContext>(),
									It.IsAny<ProductCreateRequest>(),
									It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);
		}

		[Test]
		public void ValidateRequestCustom_When_correct_storeId_Then_success_validation()
		{
			ProductCreateRequest request = ProductCreateRequestBuilder.Create()
											.WithProduct(ProductDtoBuilder.Create()
												.WithName("TEST")
												.WithCategoryId(1)
												.WithDescription("TEST DESCRIPTION")
												.WithOption(ProductOptionDtoBuilder.Create()
													.WithIndex(1)
													.WithName("TEST")
													.Build())
												.WithVariant(ProductVariantDtoBuilder.Create()
													.WithPrice(10)
													.WithQuantity(10)
													.WithOptionValue(ProductVariantOptionValueDtoBuilder.Create()
														.WithIndex(1)
														.WithValue("TEST")
														.Build())
													.Build())
												.Build())
											.Build();
			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(s => s.ValidateStoreId(It.IsAny<IDbContext>(),
														It.IsAny<ProductCreateRequest>(),
														It.IsAny<ArgumentErrorsCollection>()),
								Times.Once);
		}

		[Test]
		public void ValidateRequestCustom_When_newIds_Then_success_validation()
		{
			ProductCreateRequest request = ProductCreateRequestBuilder.Create()
											.WithProduct(ProductDtoBuilder.Create()
												.WithName("TEST")
												.WithCategoryId(1)
												.WithDescription("TEST DESCRIPTION")
												.WithOption(ProductOptionDtoBuilder.Create()
													.WithIndex(1)
													.WithName("TEST")
													.Build())
												.WithVariant(ProductVariantDtoBuilder.Create()
													.WithPrice(10)
													.WithQuantity(10)
													.WithOptionValue(ProductVariantOptionValueDtoBuilder.Create()
														.WithIndex(1)
														.WithValue("TEST")
														.Build())
													.Build())
												.Build())
											.Build();
			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(s => s.ValidateNewIds(It.IsAny<IDbContext>(),
													It.IsAny<ProductCreateRequest>(),
													It.IsAny<ArgumentErrorsCollection>()),
								Times.Once);
		}

		[Test]
		public void Execute_When_correct_request_Then_validates_Then_calls_execute()
		{
			var request = ProductCreateRequestBuilder.Create()
											.WithProduct(ProductDtoBuilder.Create()
												.WithName("TEST")
												.WithCategoryId(1)
												.WithDescription("TEST DESCRIPTION")
												.WithOption(ProductOptionDtoBuilder.Create()
													.WithIndex(1)
													.WithName("TEST")
													.Build())
												.WithVariant(ProductVariantDtoBuilder.Create()
													.WithPrice(10)
													.WithQuantity(10)
													.WithOptionValue(ProductVariantOptionValueDtoBuilder.Create()
														.WithIndex(1)
														.WithValue("TEST")
														.Build())
													.Build())
												.Build())
											.Build();
			SetupMocks(request);

			CommandMock.Setup(c => c.ValidateRequestCustom());

			CommandMock.Object.Execute(request);
			
			CommandMock.Protected().Verify("ValidateRequest", Times.Once());
			CommandMock.Verify(c => c.ValidateRequestCustom(), Times.Once);
		}

		internal override Mock<IDbContext> GetDbContextMock()
		{
			var products = new FakeDbSet<Product>();
			var stores = new FakeDbSet<Core.StoreModule.Store> {new Core.StoreModule.Store {Id = 1}};
			var dbContextMock = new Mock<IDbContext>();
			dbContextMock.Setup(c => c.Products).Returns(products);
			dbContextMock.Setup(c => c.Stores).Returns(stores);
			dbContextMock.Setup(c => c.SaveChanges()).Callback(
				() =>
					{
						//move childs for last
						var product = products.LastOrDefault();
						if (product != null)
						{
							product.Id = products.Count();
							var i = 1;
							foreach (var photo in product.Photos)
							{
								photo.ProductId = product.Id;
								photo.Id = i;
								i++;
							}
							i = 1;
							foreach (var option in product.Options)
							{
								option.ProductId = product.Id;
								option.Id = i;
								i++;
							}
							i = 1;
							foreach (var variant in product.Variants)
							{
								variant.ProductId = product.Id;
								variant.Id = i;
								i++;
							}
						}
					}
				);
			return dbContextMock;
		}
	}
}
