using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.ProductModule.Validators.EntityValidators
{
	public interface IProductValidator
	{
		ValidateArgumentResult NameMustBeUnique(string productName, int storeId);

		ValidateOperationResult IsOverMaxProductsLimit(int storeId);
	}
}