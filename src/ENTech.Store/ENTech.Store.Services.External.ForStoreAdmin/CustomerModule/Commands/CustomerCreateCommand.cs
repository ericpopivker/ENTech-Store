
using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Requests;


namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Commands
{
	public class CustomerCreateCommand : StoreAdminExternalCommandBase
			<CustomerCreateRequest, ExternalResponse, Internal.CustomerModule.Requests.CustomerCreateRequest, InternalResponse>
	{

		private readonly Internal.CustomerModule.ICustomerService _customerService;

		public CustomerCreateCommand(Internal.CustomerModule.ICustomerService customerService)
		{
			_customerService = customerService;

		}

		protected override void LimitDbContext(IDbContext dbContext)
		{
			throw new System.NotImplementedException();
		}


		public override InternalResponse ExecuteInternal(Internal.CustomerModule.Requests.CustomerCreateRequest internalRequest)
		{
			return _customerService.Create(internalRequest);
		}
	}
}