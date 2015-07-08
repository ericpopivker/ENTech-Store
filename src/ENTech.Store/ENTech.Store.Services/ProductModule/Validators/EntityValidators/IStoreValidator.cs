using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.ProductModule.Validators.EntityValidators
{
	public interface IStoreValidator
	{
		ValidateArgumentResult ValidateId(int storeId);

	}
}