using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using Moq;
using NUnit.Framework;
using ENTech.Store.Services.ProductModule.Commands;

namespace ENTech.Store.Services.UnitTests.ProductModule.Commands
{
	[TestFixture]
	public class ProductCreateCommandTest
	{
		private IUnitOfWork _unitOfWorkMock = Mock.Of<IUnitOfWork>();
		private IValidatorFactory _validatorFactoryMock = new ValidatorFactory();
		
		[Test]
		public void Validate_When_product_is_null_Then_product_is_required()
		{
			var createCommand = new ProductCreateCommand(_unitOfWorkMock, _validatorFactoryMock);
			var result = createCommand.ValidateRequest(new ProductCreateRequest());
			
			Assert.IsFalse(result.IsValid);
			Assert.IsTrue(result.ArgumentErrors["Product"].ErrorCode == CommonArgumentErrorCode.Required);

		}


		[Test]
		public void Validate_When_empty_product_name_passed_Then_product_name_is_required()
		{
			var createCommand = new ProductCreateCommand(_unitOfWorkMock, _validatorFactoryMock);

			var request = new ProductCreateRequest {Product = new ProductCreateDto()};
			var result = createCommand.ValidateRequest(request);

			Assert.IsFalse(result.IsValid);
			Assert.IsTrue(result.ArgumentErrors["Product.Name"].ErrorCode == CommonArgumentErrorCode.Required);
		}
	}
	
}
