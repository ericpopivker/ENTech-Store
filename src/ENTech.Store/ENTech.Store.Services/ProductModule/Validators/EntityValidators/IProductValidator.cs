using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.ProductModule.Validators.EntityValidators
{
	public interface IProductValidator
	{
		ValidateArgumentResult NameMustBeUnique(string argumentName, string productName);

		ValidateOperationResult IsOverMaxProductsLimit(int storeId);
	}
}