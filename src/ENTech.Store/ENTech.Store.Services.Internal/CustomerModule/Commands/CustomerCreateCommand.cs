using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.Internal.CustomerModule.Requests;
using ENTech.Store.Services.Internal.CustomerModule.Responses;

namespace ENTech.Store.Services.Internal.CustomerModule.Commands
{
	class CustomerCreateCommand : InternalCommandBase<CustomerCreateRequest, CustomerCreateResponse>
	{

		public CustomerCreateCommand(IUnitOfWork unitOfWork, ICache cache)
		{
		}

		public override void ValidateRequestCustom()
		{
		}

		protected override void Execute()
		{
				
		}
	}
}
