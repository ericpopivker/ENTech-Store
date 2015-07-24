using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.GeoModule.EntityValidators
{
	public interface IAddressValidator
	{
		ValidateArgumentResult ValidateId(int addressId);
	}
}