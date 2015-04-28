using ENTech.Store.Infrastructure.Services.Factories;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Internal.CustomerModule.Commands;
using ENTech.Store.Services.Internal.CustomerModule.Requests;

namespace ENTech.Store.Services.Internal.CustomerModule
{
	class CustomerService : ICustomerService
	{

		public InternalResponse Create(CustomerCreateRequest request)
		{
			var factory = new InternalCommandFactory();
			var command = factory.Create<CustomerCreateCommand>();
			var response = command.Execute(request);
			return response;
		}
	}
}