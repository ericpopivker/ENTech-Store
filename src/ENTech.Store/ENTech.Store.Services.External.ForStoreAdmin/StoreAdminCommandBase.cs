using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.External.ForStoreAdmin
{
	public abstract class StoreAdminExternalCommandBase<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse> 
		: ExternalCommandBase<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse>
		where TExternalRequest : ExternalRequestBase,IExternalRequest
		where TExternalResponse : ExternalResponse, new() 
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : InternalResponse
	{
		protected override bool Authenticate()
		{
			return true; //for now
		}

	}
}
