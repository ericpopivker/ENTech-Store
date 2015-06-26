using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.ProductModule.Validators
{
	public interface IProductValidator
	{
		ValidatorResult IsOverMaxProductsLimit(int storeId);
		ArgumentValidatorResult NameMustBeUnique(string argumentName, string productName);

	}
}