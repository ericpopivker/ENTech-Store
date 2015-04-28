using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Core;
using ENTech.Store.Core.StoreModule.Products;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.StoreModule.Commands;
using ENTech.Store.Services.Internal.StoreModule.Requests;
using ENTech.Store.Services.Internal.StoreModule.Responses;
using ENTech.Store.Services.Internal.StoreModule.Validators;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Internal.UnitTests.StoreModule.Commands
{
	[TestFixture]
	public class ProductDeleteCommandTest :
		CommandTestBase
			<IDbContext, ProductDeleteCommand, ProductDeleteRequest, ProductDeleteResponse, ProductRequestCustomValidator,
			ProductRequestCustomValidatorErrorCode>
	{
		private readonly FakeDbSet<Product> _products = new FakeDbSet<Product>();

		[SetUp]
		public void RegisterErrorCodes()
		{
			RequestValidatorErrorMessagesDictionary.Register<ProductRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<RequestValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<CommonErrorCode>();
		}

		[Test]
		public void ValidateRequestCustom_When_correct_productId_Then_success_validation()
		{
			var request = new ProductDeleteRequest {StoreId = 1, ProductId = 1};

			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(
				s => s.ValidateProductId(It.IsAny<IDbContext>(),
										It.IsAny<ProductDeleteRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			Assert.IsFalse(ErrorsMock.Object.HasArgument("ProductId"));
		}

		[Test]
		public void ValidateRequestCustom_When_correct_storeId_Then_success_validation()
		{
			var request = new ProductDeleteRequest { StoreId = 1, ProductId = 1 };

			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(
				s => s.ValidateStoreId(It.IsAny<IDbContext>(),
										It.IsAny<ProductDeleteRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			Assert.IsFalse(ErrorsMock.Object.HasArgument("StoreId"));
		}

		[Test]
		public void Execute_When_correct_request_Then_soft_delte_products_Then_save_changes_Then_success_response()
		{
			var request = new ProductDeleteRequest { StoreId = 1, ProductId = 1 };

			SetupMocks(request);

			CommandMock.Object.Execute(request);

			DbContextMock.VerifyGet(d => d.Products);
			DbContextMock.Verify(c => c.SaveChanges(), Times.Once);

			var product = _products.First();

			Assert.IsTrue(product.IsDeleted && product.DeletedAt.HasValue);
			foreach (var productVariant in product.Variants)
			{
				Assert.IsTrue(productVariant.IsDeleted && productVariant.DeletedAt.HasValue);
			}
		}

		internal override Mock<IDbContext> GetDbContextMock()
		{
			_products.Clear();
			_products.Add(
				new Product
					{
						Id = 1,
						IsDeleted = false,
						DeletedAt = null,
						Variants = new List<ProductVariant>
							{
								new ProductVariant
									{
										Id = 1,
										IsDeleted = false,
										DeletedAt = null
									}
							}
					});

			var stores = new FakeDbSet<Core.StoreModule.Store> {new Core.StoreModule.Store {Id = 1}};
			var dbContextMock = new Mock<IDbContext>();
			dbContextMock.Setup(c => c.Products).Returns(_products);
			dbContextMock.Setup(c => c.Stores).Returns(stores);
			dbContextMock.Setup(c => c.SaveChanges()).Returns(1).Callback(
				() =>
					{
						//move childs for last
						var product = _products.LastOrDefault();
						if (product != null)
						{
							product.Id = _products.Count();
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
