using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Commands;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Requests;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Responses;

namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule
{
	public class CustomerService : ICustomerService
	{
		public ExternalResponse Create(CustomerCreateRequest request)
		{
			var factory = new ExternalCommandFactory();
			var command = factory.Create<CustomerCreateCommand>();
			return command.Execute(request);
		}
	}
}
