using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Requests;

namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule
{
	public interface ICustomerService
	{
		ExternalResponse Create(CustomerCreateRequest request);
	}
}