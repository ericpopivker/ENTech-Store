using System.Linq;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Validators.DtoValidators
{
	[TestFixture]
	public class ProductCreateRequestValidatorTest
	{
		[Test]
		public void Validate_When_product_is_null_Then_product_is_required()
		{
			var request = new ProductCreateRequest { Product = null };
			var productCreateRequestValidator = new ProductCreateRequestValidator();

			var validatorResult = productCreateRequestValidator.Validate(request);

			Assert.IsFalse(validatorResult.IsValid);
			var argErrors = validatorResult.ArgumentErrors;

			Assert.IsTrue(argErrors.Any(e => e.ArgumentName == "Product" && e.ArgumentError.ErrorCode == CommonArgumentErrorCode.Required && e.ArgumentError.ErrorMessage == "Required"));
		}


		[Test]
		public void Validate_When_has_nested_object_errors_Then_those_errors_are_not_returned()
		{
			var validator = new ProductCreateRequestValidator();
			var request = new ProductCreateRequest { Product = new  ProductCreateDto() };
			var validatorResult = validator.Validate(request);

			Assert.IsTrue(validatorResult.IsValid);
		}
	}
}
