using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Internal.CustomerModule.Requests;

namespace ENTech.Store.Services.Internal.CustomerModule
{
	public interface ICustomerService
	{
		InternalResponse Create(CustomerCreateRequest request);
	}
}