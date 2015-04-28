using System;
using System.Collections.Generic;
using ENTech.Store.Core;
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
using NUnit.Framework;

namespace ENTech.Store.Services.Internal.UnitTests.StoreModule.Commands
{
	[TestFixture]
	public class ProductsFindCommandTest :
		CommandTestBase
			<IDbContext, ProductFindCommand, ProductFindRequest, ProductFindResponse, ProductRequestCustomValidator,
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
		public void ValidateRequestCustom_When_correct_categoryId_Then_success_validation()
		{
			ProductFindRequest request = ProductFindRequestBuilder.Create()
											.WithStoreId(1)
											.WithCriteria(ProductCriteriaDtoBuilder.Create()
												.WithCategoryId(1)
												.WithSortField1(SortFieldDtoBuilder.Create()
													.WithField("Name")
													.Build())
												.Build())
											.Build();

			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();
			ValidatorMock.Verify(
				s => s.ValidateCategoryId(It.IsAny<IDbContext>(),
										It.IsAny<ProductFindRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			DbContextMock.VerifyGet(c => c.ProductCategories, Times.Once);

			Assert.IsFalse(ErrorsMock.Object.HasArgument("Criteria.CategoryId"));
		}

		[Test]
		public void ValidateRequestCustom_When_wrong_categoryId_Then_fail_validation_And_add_ArgumentError()
		{
			var request = ProductFindRequestBuilder.Create()
											.WithStoreId(1)
											.WithCriteria(ProductCriteriaDtoBuilder.Create()
												.WithCategoryId(10)
												.WithSortField1(SortFieldDtoBuilder.Create()
													.WithField("Name")
													.Build())
												.Build())
											.Build();

			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();

			ValidatorMock.Verify(
				s => s.ValidateCategoryId(It.IsAny<IDbContext>(),
										It.IsAny<ProductFindRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			DbContextMock.VerifyGet(c => c.ProductCategories, Times.Once);

			Assert.IsTrue(ErrorsMock.Object.HasArgument("Criteria.CategoryId"));
		}

		[Test]
		public void ValidateRequestCustom_When_correct_storeId_Then_success_validation()
		{
			var request = ProductFindRequestBuilder.Create()
											.WithStoreId(1)
											.WithCriteria(ProductCriteriaDtoBuilder.Create()
												.WithCategoryId(1)
												.WithSortField1(SortFieldDtoBuilder.Create()
													.WithField("Name")
													.Build())
												.Build())
											.Build();
			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();
			ValidatorMock.Verify(
				s => s.ValidateStoreId(It.IsAny<IDbContext>(),
										It.IsAny<ProductFindRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			DbContextMock.VerifyGet(c => c.Stores, Times.Once);

			Assert.IsFalse(ErrorsMock.Object.HasArgument("StoreId"));
		}

		[Test]
		public void ValidateRequestCustom_When_wrong_storeId_Then_fail_validation_And_add_ArgumentError()
		{

			var request = ProductFindRequestBuilder.Create()
											.WithStoreId(10)
											.WithCriteria(ProductCriteriaDtoBuilder.Create()
												.WithCategoryId(1)
												.WithSortField1(SortFieldDtoBuilder.Create()
													.WithField("Name")
													.Build())
												.Build())
											.Build();

			SetupMocks(request);

			CommandMock.Object.ValidateRequestCustom();
			ValidatorMock.Verify(
				s => s.ValidateStoreId(It.IsAny<IDbContext>(),
										It.IsAny<ProductFindRequest>(),
										It.IsAny<ArgumentErrorsCollection>()),
				Times.Once);

			DbContextMock.VerifyGet(c => c.Stores, Times.Once);

			Assert.IsTrue(ErrorsMock.Object.HasArgument("StoreId"));
		}

		[Test]
		public void ValidateRequestCustom_When_wrong_dto_Then_fail_dto_validation_And_add_ArgumentError()
		{
			var request = new ProductFindRequest();

			SetupMocks(request);

			var response = CommandMock.Object.Execute(request);

			foreach (var error in response.ArgumentErrors)
			{
				Console.WriteLine(error.ArgumentName + " : " + error.ErrorCode + " : " + error.ErrorMessage);
			}

			Assert.IsTrue(ErrorsMock.Object.HasArgument("StoreId"));
		}

		[Test]
		public void Execute_When_correct_request_Then_gets_products_And_returns_success_response()
		{
			var request = ProductFindRequestBuilder.Create()
											.WithStoreId(1)
											.WithCriteria(ProductCriteriaDtoBuilder.Create()
												.WithCategoryId(1)
												.WithPageIndex(1)
												.WithPageSize(10)
												.WithSortField1(SortFieldDtoBuilder.Create()
													.WithField("Name")
													.Build())
												.Build())
											.Build();

			SetupMocks(request);

			CommandMock.Object.Execute(request);

			DbContextMock.VerifyGet(c => c.Products, Times.Once);

			Assert.IsTrue(ResponseMock.Object.IsSuccess);
		}

		internal override Mock<IDbContext> GetDbContextMock()
		{
			var stores = new FakeDbSet<Core.StoreModule.Store> {new Core.StoreModule.Store {Id = 1}};
			var categories = new FakeDbSet<ProductCategory> {new ProductCategory {Id = 1}};
			_products.Clear();
			_products.Add(
				new Product
					{
						Id = 1,
						CategoryId = 1,
						Name = "A",
						Variants = new List<ProductVariant>
							{
								new ProductVariant {Id = 1, Sku = "AAA"},
								new ProductVariant {Id = 2, Sku = "ZZZ"}
							}
					});

			_products.Add(
				new Product
					{
						Id = 2,
						CategoryId = 1,
						Name = "A",
						Variants = new List<ProductVariant>
							{
								new ProductVariant {Id = 3, Sku = "ABA"},
								new ProductVariant {Id = 4, Sku = "AZZ"}
							}
					});

			var dbContextMock = new Mock<IDbContext>();
			dbContextMock.Setup(c => c.Products).Returns(_products);
			dbContextMock.Setup(c => c.Stores).Returns(stores);
			dbContextMock.Setup(c => c.ProductCategories).Returns(categories);
			return dbContextMock;
		}
	}
}
