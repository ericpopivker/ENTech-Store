using System.Linq;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Validators.DtoValidators
{
	[TestFixture]
	public class ProductCreateDtoValidatorTest
	{
		[Test]
		public void Validate_When_product_name_is_empty_Then_product_name_is_required()
		{
			var validator= new ProductCreateDtoValidator();
			var validatorResult = validator.Validate(new ProductCreateDto());

			Assert.IsFalse(validatorResult.IsValid);
			var argErrors = validatorResult.ArgumentErrors;

			Assert.IsTrue(argErrors.Any(e => e.ArgumentName == "Name" && e.ErrorCode == CommonArgumentErrorCode.Required && e.ErrorMessage == "Required"));
		}
	}
}
